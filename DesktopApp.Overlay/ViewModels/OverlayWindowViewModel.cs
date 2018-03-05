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
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Overlay.Draw.Models;

namespace DesktopApp.ViewModels
{
    public class OverlayWindowViewModel : ViewModelBase, IReactionInteraction
    {
        private readonly IReactionHubProxy ReactionHub;

        public OverlayWindowViewModel(IReactionHubProxy reactionHubProxy)
        {
            this.ReactionHub = reactionHubProxy;
            this.ReactionHub.Connected += async () =>
            {
                this.ReactionHub.OnReceiveReaction()
                .Subscribe(x => this.OnInteraction(x.Item1, x.Item2))
                .AddTo(this.Disposable);
                var ret = await this.ReactionHub.AddListener();
                if ( ret.ResultTypes == eResultTypes.Failed ) {
                    throw new ArgumentException( "PresentationID が不正です" );
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
