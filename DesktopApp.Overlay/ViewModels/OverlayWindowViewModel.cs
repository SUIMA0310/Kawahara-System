using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using DesktopApp.Services;

namespace DesktopApp.ViewModels
{
    public class OverlayWindowViewModel : ViewModelBase, IWindowController
    {

        private IWindowService WindowService;

        public ReactiveProperty<double> WindowOpacity { get; }

        public OverlayWindowViewModel(IWindowService windowService)
        {

            this.WindowService = windowService;
            //this.WindowService.SetWindowController(this);

            this.WindowOpacity = new ReactiveProperty<double>(1.0);

            this.WindowInitialized?.Invoke();
        }


        public event Action WindowInitialized;
        public event Action WindowClosed;
        public event Action<bool> HiddenChanged;

        public void Close()
        {
            this.WindowClosed?.Invoke();
        }
        bool IWindowController.IsHidden
        {
            get => this.WindowOpacity.Value == 0.0;
            set {

                if ( value ) {

                    this.WindowOpacity.Value = 0.0;

                } else {

                    this.WindowOpacity.Value = 1.0;

                }

                HiddenChanged?.Invoke( value );

            }
        }
    }
}
