namespace DesktopApp.Services
{
    public class WindowService : WindowServiceBase<IWindowFactory, IWindowController>
    {
        protected override void OnWindowControllerChanged( EventArgs<IWindowController> eventArgs )
        {
            base.OnWindowControllerChanged( eventArgs );
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}