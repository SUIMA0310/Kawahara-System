using System;
using System.Reactive.Disposables;
using System.Reflection;
using System.Linq;

using DesktopApp.Models;
using DesktopApp.Overlay.Draw.Models;
using DesktopApp.Overlay.Draw.Helpers;

using Reactive.Bindings;

namespace DesktopApp.Services
{
    public class DisplayControlService : IDisposable, IDisplayControlService
    {
        public ReactiveProperty<float>           DisplayTime  { get; }
        public ReactiveProperty<float>           MaxOpacity   { get; }
        public ReactiveProperty<float>           Scale        { get; }
        public ReactiveProperty<IParameterCurve> MoveMethod   { get; }
        public ReactiveProperty<IParameterCurve> OpacityCurve { get; }

        private readonly IDisplaySettingsStore SettingsStore;

        public DisplayControlService( IDisplaySettingsStore settingsStore )
        {
            this.SettingsStore = settingsStore;

            this.MaxOpacity  = new ReactiveProperty<float>( this.SettingsStore.MaxOpacity ) ;
            this.Scale       = new ReactiveProperty<float>( this.SettingsStore.Scale )      ;
            this.DisplayTime = new ReactiveProperty<float>( this.SettingsStore.DisplayTime );

            this.MoveMethod = new ReactiveProperty<IParameterCurve>(
                    this.SettingsStore.MoveMethodName.GetParameterCurveInstance( false )   ?? Constant.Instance );
            this.OpacityCurve = new ReactiveProperty<IParameterCurve>(
                    this.SettingsStore.OpacityCurveName.GetParameterCurveInstance( false ) ?? Constant.Instance );
        }

        public void Load()
        {
            this.MaxOpacity.Value  = this.SettingsStore.MaxOpacity ;
            this.Scale.Value       = this.SettingsStore.Scale      ;
            this.DisplayTime.Value = this.SettingsStore.DisplayTime;

            this.MoveMethod.Value
                = this.SettingsStore.MoveMethodName.GetParameterCurveInstance( false )   ?? Constant.Instance;
            this.OpacityCurve.Value
                = this.SettingsStore.OpacityCurveName.GetParameterCurveInstance( false ) ?? Constant.Instance;
        }

        public void Save()
        {
            this.SettingsStore.MaxOpacity       = this.MaxOpacity.Value                     ;
            this.SettingsStore.Scale            = this.Scale.Value                          ;
            this.SettingsStore.DisplayTime      = this.DisplayTime.Value                    ;

            this.SettingsStore.MoveMethodName   = this.MoveMethod.Value.GetType().FullName  ;
            this.SettingsStore.OpacityCurveName = this.OpacityCurve.Value.GetType().FullName;

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