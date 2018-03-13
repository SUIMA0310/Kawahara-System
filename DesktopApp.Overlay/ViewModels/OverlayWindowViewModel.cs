using System;
using System.Reactive.Linq;

using DesktopApp.Models;
using DesktopApp.Overlay.Draw.Models;
using DesktopApp.Services;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace DesktopApp.ViewModels
{
    public class OverlayWindowViewModel : ViewModelBase, IReactionInteraction
    {
        public ReactiveProperty<float> DisplayTime            { get; }
        public ReactiveProperty<float> MaxOpacity             { get; }
        public ReactiveProperty<float> Scale                  { get; }
        public ReactiveProperty<IParameterCurve> MoveMethod   { get; }
        public ReactiveProperty<IParameterCurve> OpacityCurve { get; }

        private readonly IReactionHubProxy ReactionHub;
        private readonly IDisplayControlService DisplayControl;

        public OverlayWindowViewModel( IReactionHubProxy reactionHubProxy, IDisplayControlService displayControl )
        {
            this.ReactionHub    = reactionHubProxy;
            this.DisplayControl = displayControl;

            this.DisplayTime  = this.DisplayControl.DisplayTime
                                    .ToReactiveProperty()
                                    .AddTo( this.Disposable );
            this.MaxOpacity   = this.DisplayControl.MaxOpacity
                                    .ToReactiveProperty()
                                    .AddTo( this.Disposable );
            this.Scale        = this.DisplayControl.Scale
                                    .ToReactiveProperty()
                                    .AddTo( this.Disposable );
            this.MoveMethod   = this.DisplayControl.MoveMethod
                                    .ToReactiveProperty()
                                    .AddTo( this.Disposable );
            this.OpacityCurve = this.DisplayControl.OpacityCurve
                                    .ToReactiveProperty()
                                    .AddTo( this.Disposable );

            this.ReactionHub.Connected += async () =>
            {
                //リアクションの受信設定
                this.ReactionHub.OnReceiveReaction()
                                .Subscribe( x => this.OnInteraction( x.Item1, x.Item2 ) )
                                .AddTo( this.Disposable );

                //リスナー登録
                var ret = await this.ReactionHub.AddListener();
                if ( ret.ResultTypes == eResultTypes.Failed ) {
                    throw new ArgumentException( ret.Message );
                }
            };
            this.ReactionHub.Open();
        }

        public override void Dispose()
        {
            this.ReactionHub?.RemoveListener();
            base.Dispose();
        }

        private void OnInteraction( eReactionType reactionType, Color color )
        {
            this.Interaction?.Invoke( reactionType, color );
        }

        public event Action<eReactionType, Color> Interaction;
    }
}