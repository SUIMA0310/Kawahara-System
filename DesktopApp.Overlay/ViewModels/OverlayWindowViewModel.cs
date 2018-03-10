using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Overlay.Draw.Models;

namespace DesktopApp.ViewModels
{
    public class OverlayWindowViewModel : ViewModelBase, IReactionInteraction
    {

        public ReactiveProperty<float> DisplayTime { get; }
        public ReactiveProperty<float> MaxOpacity { get; }
        public ReactiveProperty<float> Scale { get; }

        private readonly IReactionHubProxy ReactionHub;
        private readonly IDisplayControlService DisplayControl;
        
        public OverlayWindowViewModel(IReactionHubProxy reactionHubProxy, IDisplayControlService displayControl)
        {

            this.ReactionHub = reactionHubProxy;
            this.DisplayControl = displayControl;

            this.DisplayTime = this.DisplayControl.DisplayTime
                                .ToReactiveProperty()
                                .AddTo(this.Disposable);
            this.MaxOpacity = this.DisplayControl.MaxOpacity
                                .ToReactiveProperty()
                                .AddTo(this.Disposable);
            this.Scale = this.DisplayControl.Scale
                                .ToReactiveProperty()
                                .AddTo(this.Disposable);

            this.ReactionHub.Connected += async () =>
            {
                //リアクションの受信設定
                this.ReactionHub.OnReceiveReaction()
                                .Subscribe(x => this.OnInteraction(x.Item1, x.Item2))
                                .AddTo(this.Disposable);

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

        private void OnInteraction(eReactionType reactionType, Color color)
        {
            this.Interaction?.Invoke(reactionType, color);
        }

        public event Action<eReactionType, Color> Interaction;
    }
}
