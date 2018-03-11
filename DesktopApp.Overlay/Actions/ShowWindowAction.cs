using System;
using System.Windows;
using System.Windows.Interactivity;

using Microsoft.Practices.ServiceLocation;

namespace DesktopApp.Actions
{
    public class ShowWindowAction : TriggerAction<FrameworkElement>
    {
        public Type WindowType
        {
            get { return (Type)GetValue( WindowTypeProperty ); }
            set { SetValue( WindowTypeProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for WindowType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WindowTypeProperty =
            DependencyProperty.Register("WindowType", typeof(Type), typeof(ShowWindowAction), new PropertyMetadata(null));

        protected override void Invoke( object parameter )
        {
            if ( this.WindowType == null ) {
                return;
            }

            Window wrapperWindow = this.CreateWindow();
            if ( wrapperWindow != null ) {
                wrapperWindow.Show();
            }
        }

        protected virtual Window CreateWindow()
        {
            var container = ServiceLocator.Current.GetInstance<DryIoc.IContainer>();
            return container.Resolve( this.WindowType, false ) as Window;
        }
    }
}