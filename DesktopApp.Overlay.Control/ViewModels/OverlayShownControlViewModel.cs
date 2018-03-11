using System;
using System.Reactive.Linq;

using DesktopApp.Services;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    public class OverlayShownControlViewModel : ViewModelBase
    {
        public ReactiveProperty<float> DisplayTime { get; }
        public ReactiveProperty<float> MaxOpacity { get; }
        public ReactiveProperty<float> Scale { get; }

        public ReactiveCommand SaveCommand { get; }
        public ReactiveCommand ResetCommand { get; }

        private readonly IDisplayControlService DisplayControl;

        public OverlayShownControlViewModel( IDisplayControlService displayControl )
        {
            this.DisplayControl = displayControl;

            this.DisplayTime = this.DisplayControl.DisplayTime;
            this.MaxOpacity = this.DisplayControl.MaxOpacity;
            this.Scale = this.DisplayControl.Scale;

            this.SaveCommand = new ReactiveCommand();
            this.SaveCommand
                .Subscribe( _ =>
             {
                 this.DisplayControl.Save();
             } )
            .AddTo( this.Disposable );

            this.ResetCommand = new ReactiveCommand();
            this.ResetCommand
                .Subscribe( _ =>
             {
                 this.DisplayControl.Load();
             } )
            .AddTo( this.Disposable );

            this.Title.Value = "Display control";
        }
    }
}