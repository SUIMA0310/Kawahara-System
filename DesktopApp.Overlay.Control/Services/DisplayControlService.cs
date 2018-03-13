using System;
using System.Reactive.Disposables;
using System.Reflection;
using System.Linq;

using DesktopApp.Models;
using DesktopApp.Overlay.Draw.Models;

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
                    this.GetParameterCurve( this.SettingsStore.MoveMethodName, false )   ?? Constant.Instance );
            this.OpacityCurve = new ReactiveProperty<IParameterCurve>(
                    this.GetParameterCurve( this.SettingsStore.OpacityCurveName, false ) ?? Constant.Instance );
        }

        public void Load()
        {
            this.MaxOpacity.Value  = this.SettingsStore.MaxOpacity ;
            this.Scale.Value       = this.SettingsStore.Scale      ;
            this.DisplayTime.Value = this.SettingsStore.DisplayTime;

            this.MoveMethod.Value
                = this.GetParameterCurve( this.SettingsStore.MoveMethodName, false )   ?? Constant.Instance;
            this.OpacityCurve.Value
                = this.GetParameterCurve( this.SettingsStore.OpacityCurveName, false ) ?? Constant.Instance;
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

        private IParameterCurve GetParameterCurve( string className, bool throwOnError = true )
        {
            try {

                //型情報を取得
                var type = AppDomain.CurrentDomain
                                    .GetAssemblies()
                                    .AsParallel()
                                    .Select( a => a.GetType( className ) )
                                    .Where( x => x != null )
                                    .First();

                //型がinterfaceを実装しているか確認
                var ifType = type.GetInterface( nameof( IParameterCurve ) );
                if ( ifType == null ) {
                    throw new ArgumentException( $"{className} は、{nameof( IParameterCurve )}を実装しません." );
                }

                // Instance プロパティ情報を取得
                var propInfo = type.GetProperty( "Instance", BindingFlags.Static | BindingFlags.Public );
                if ( propInfo == null ) {
                    throw new ArgumentException( $"{className} は、Instance Propertyを実装しません." );
                }

                //プロパティから値を取得
                var instance = propInfo.GetValue( null ) as IParameterCurve;
                if ( instance == null ) {
                    throw new AggregateException( $"{className}.Instance の戻り値を取得できません." );
                }

                return instance;
            } catch ( Exception ex ) {
                if ( throwOnError ) {
                    throw new ArgumentException( $"{className} のインスタンス取得に失敗しました.", ex );
                }

                return null;
            }
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