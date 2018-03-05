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
using DesktopApp.Overlay.Draw.Models;
using DesktopApp.Models;

namespace DesktopApp.ViewModels
{
    public class OverlayWindowViewModel : ViewModelBase, IReactionInteraction
    {
        public OverlayWindowViewModel()
        {

            Observable.Timer(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1.1))
                .Subscribe(x => this.OnInteraction(eReactionType.Good, new Color(255, 150, 150)))
                .AddTo(this.Disposable);

            Observable.Timer(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2.6))
                .Subscribe(x => this.OnInteraction(eReactionType.Good, new Color(150, 255, 150)))
                .AddTo(this.Disposable);

            Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5.2))
                .Subscribe(x => this.OnInteraction(eReactionType.Good, new Color(150, 150, 255)))
                .AddTo(this.Disposable);

        }

        private void OnInteraction(eReactionType reactionType, Color color)
        {
            this.Interaction?.Invoke(reactionType, color);
        }

        public event Action<eReactionType, Color> Interaction;
    }
}
