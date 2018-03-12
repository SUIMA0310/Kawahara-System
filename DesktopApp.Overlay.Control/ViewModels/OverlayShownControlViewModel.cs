using System;
using System.Collections.Generic;
using System.Reactive.Linq;

using DesktopApp.Services;
using DesktopApp.Overlay.Draw.Models;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    public class OverlayShownControlViewModel : ViewModelBase
    {
        public ReactiveProperty<float>  DisplayTime  { get; }
        public ReactiveProperty<float>  MaxOpacity   { get; }
        public ReactiveProperty<float>  Scale        { get; }
        public ReactiveProperty<IParameterCurve> MoveMethod   { get; }
        public ReactiveProperty<IParameterCurve> OpacityCurve { get; }

        public ReactiveCommand SaveCommand           { get; }
        public ReactiveCommand ResetCommand          { get; }

        public List<IParameterCurve> CurveOptions    { get; }

        private readonly IDisplayControlService DisplayControl;

        public OverlayShownControlViewModel( IDisplayControlService displayControl )
        {
            this.DisplayControl = displayControl;

            this.DisplayTime  = this.DisplayControl.DisplayTime ;
            this.MaxOpacity   = this.DisplayControl.MaxOpacity  ;
            this.MoveMethod   = this.DisplayControl.MoveMethod  ;
            this.OpacityCurve = this.DisplayControl.OpacityCurve;
            this.Scale        = this.DisplayControl.Scale       ;

            this.CurveOptions = new List<IParameterCurve>();
            this.CurveOptions.Add( Constant.Instance  );
            this.CurveOptions.Add( Quadratic.Instance );

            this.SaveCommand = new ReactiveCommand();
            this.SaveCommand
            .Subscribe( _ => { this.DisplayControl.Save(); } )
            .AddTo( this.Disposable );

            this.ResetCommand = new ReactiveCommand();
            this.ResetCommand
            .Subscribe( _ => { this.DisplayControl.Load(); } )
            .AddTo( this.Disposable );

            this.Title.Value = "Display control";
        }
    }
}