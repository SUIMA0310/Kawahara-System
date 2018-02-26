using Prism.Mvvm;

namespace DesktopApp.ViewModels
{
    public class ShellViewModel : Core.ViewModels.ViewModelBase
    {
        
        public ShellViewModel()
        {

            this.Title.Value = "Prism Application";

        }
    }
}
