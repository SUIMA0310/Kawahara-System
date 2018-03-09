using Microsoft.Practices.ServiceLocation;
using Prism.Common;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.Actions
{
    public class PopupMetroWindowAction : PopupWindowAction
    {

        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null) {
                return;
            }

            // If the WindowContent shouldn't be part of another visual tree.
            if (this.WindowContent != null && this.WindowContent.Parent != null) {
                return;
            }

            Window wrapperWindow = this.GetWindow(args.Context);

            // We invoke the callback when the interaction's window is closed.
            var callback = args.Callback;
            void handler(object o, EventArgs e)
            {
                wrapperWindow.Closed -= handler;
                if (wrapperWindow is Views.Dialogs.PopupMetroWindow metroWindow) {
                    metroWindow.MainControl.Content = null;
                } else {
                    wrapperWindow.Content = null;
                }
                callback?.Invoke();
            }

            wrapperWindow.Closed += handler;

            if (this.CenterOverAssociatedObject && this.AssociatedObject != null) {
                // If we should center the popup over the parent window we subscribe to the SizeChanged event
                // so we can change its position after the dimensions are set.
                void sizeHandler(object o, SizeChangedEventArgs e)
                {
                    wrapperWindow.SizeChanged -= sizeHandler;

                    // If the parent window has been minimized, then the poition of the wrapperWindow is calculated to be off screen
                    // which makes it impossible to activate and bring into view.  So, we want to check to see if the parent window
                    // is minimized and automatically set the position of the wrapperWindow to be center screen.
                    var parentWindow = wrapperWindow.Owner;
                    if (parentWindow != null && parentWindow.WindowState == WindowState.Minimized) {
                        wrapperWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                        return;
                    }

                    FrameworkElement view = this.AssociatedObject;

                    // Position is the top left position of the view from which the request was initiated.
                    // On multiple monitors, if the X or Y coordinate is negative it represent that the monitor from which
                    // the request was initiated is either on the left or above the PrimaryScreen
                    Point position = view.PointToScreen(new Point(0, 0));
                    var source = PresentationSource.FromVisual(view);
                    position = source.CompositionTarget.TransformFromDevice.Transform(position);

                    // Find the middle of the calling view.
                    // Take the width and height of the view divided by 2 and add to the X and Y coordinates.
                    var middleOfView = new Point(position.X + (view.ActualWidth / 2),
                                                 position.Y + (view.ActualHeight / 2));

                    // Set the coordinates for the top left part of the wrapperWindow.
                    // Take the width of the wrapperWindow, divide it by 2 and substract it from 
                    // the X coordinate of middleOfView. Do the same thing for the Y coordinate.
                    // If the wrapper window is wider or taller than the view, it will be behind the view.
                    wrapperWindow.Left = middleOfView.X - (wrapperWindow.ActualWidth / 2);
                    wrapperWindow.Top = middleOfView.Y - (wrapperWindow.ActualHeight / 2);

                }

                wrapperWindow.SizeChanged += sizeHandler;
            }

            if (wrapperWindow is Views.Dialogs.PopupMetroWindow metroWindow2) {

                var grind = metroWindow2.MainControl.Content as FrameworkElement;
                metroWindow2.Width = grind.Width;
                metroWindow2.Height = grind.Height + 34.0f;

            }

            if (this.IsModal) {
                wrapperWindow.ShowDialog();
            } else {
                wrapperWindow.Show();
            }
        }

        protected override Window CreateWindow()
        {
            return new Views.Dialogs.PopupMetroWindow();
        }

        protected override void PrepareContentForWindow(INotification notification, Window wrapperWindow)
        {
            void setNotificationAndClose(IInteractionRequestAware iira)
            {
                iira.Notification = notification;
                iira.FinishInteraction = () => wrapperWindow.Close();
            }

            if (this.WindowContent != null) {
                // We set the WindowContent as the content of the window. 
                if (wrapperWindow is Views.Dialogs.PopupMetroWindow metroWindow) {
                    metroWindow.MainControl.Content = this.WindowContent;
                    MvvmHelpers.ViewAndViewModelAction<IInteractionRequestAware>(metroWindow.MainControl.Content, setNotificationAndClose);
                    return;
                } else {
                    wrapperWindow.Content = this.WindowContent;
                }
            } else if (this.WindowContentType != null) {
                wrapperWindow.Content = ServiceLocator.Current.GetInstance(this.WindowContentType);
            } else {
                return;
            }

            MvvmHelpers.ViewAndViewModelAction<IInteractionRequestAware>(wrapperWindow.Content, setNotificationAndClose);
        }
    }
}
