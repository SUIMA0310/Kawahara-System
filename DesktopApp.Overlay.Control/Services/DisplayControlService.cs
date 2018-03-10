using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using DesktopApp.Models;

namespace DesktopApp.Services
{
    public class DisplayControlService : IDisposable, IDisplayControlService
    {

        public ReactiveProperty<float> DisplayTime { get; }
        public ReactiveProperty<float> MaxOpacity { get; }
        public ReactiveProperty<float> Scale { get; }

        private readonly IDisplaySettingsStore SettingsStore;

        public DisplayControlService(IDisplaySettingsStore settingsStore)
        {
            this.SettingsStore = settingsStore;

            this.MaxOpacity = new ReactiveProperty<float>(this.SettingsStore.MaxOpacity);
            this.Scale = new ReactiveProperty<float>(this.SettingsStore.Scale);
            this.DisplayTime = new ReactiveProperty<float>(this.SettingsStore.DisplayTime);
        }

        public void Load()
        {
            this.MaxOpacity.Value = this.SettingsStore.MaxOpacity;
            this.Scale.Value = this.SettingsStore.Scale;
            this.DisplayTime.Value = this.SettingsStore.DisplayTime;
        }

        public void Save()
        {
            this.SettingsStore.MaxOpacity = this.MaxOpacity.Value;
            this.SettingsStore.Scale = this.Scale.Value;
            this.SettingsStore.DisplayTime = this.DisplayTime.Value;

            this.SettingsStore.Save();
        }

        /// <summary>
        /// IDisposableをまとめるコンテナ
        /// </summary>
        protected CompositeDisposable Disposable { get; } = new CompositeDisposable();

        /// <summary>
        /// 破棄処理を行う
        /// </summary>
        public virtual void Dispose()
        {
            //まとめてDisposeする
            this.Disposable.Dispose();
        }

    }
}
