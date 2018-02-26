using Prism.Mvvm;

namespace DesktopApp.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ShellViewModel()
        {

        }
    }
}
