using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace DesktopApp.Behaviors
{
    public class ViewModelCleanupBehavior
    {

        // Using a DependencyProperty as the backing store for TargetCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetCollectionProperty =
            DependencyProperty.RegisterAttached(
                "TargetCollection",
                typeof(bool),
                typeof(ViewModelCleanupBehavior),
                new PropertyMetadata(ActivateOnLoadPropertyChanged));

        public static bool GetTargetCollection(DependencyObject obj)
        {
            return (bool)obj.GetValue(TargetCollectionProperty);
        }

        public static void SetTargetCollection(DependencyObject obj, bool value)
        {
            obj.SetValue(TargetCollectionProperty, value);
        }

        private static void ActivateOnLoadPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {

            if (DesignerProperties.GetIsInDesignMode(obj)) {

                return;

            }

            if (obj is FrameworkElement element) {

                element.Unloaded -= ElementUnloaded;

                if ((bool)args.NewValue) {
                    element.Unloaded += ElementUnloaded;
                }

            }

        }

        private static void ElementUnloaded(object sender, RoutedEventArgs e)
        {
            if ( sender is FrameworkElement element ) {

                if ( element.DataContext is IDisposable disposable ) {

                    disposable.Dispose();

                }

            }
        }

    }
}
