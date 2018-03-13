using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Overlay.Draw.Models;

namespace DesktopApp.Overlay.Draw.Helpers
{
    public static class ParameterCurveHelpers
    {
        public static IParameterCurve GetParameterCurveInstance( this string className, bool throwOnError = true )
        {
            Type type = null;
            try {
                //型情報を取得
                type = AppDomain.CurrentDomain
                                .GetAssemblies()
                                .AsParallel()
                                .Select( a => a.GetType( className ) )
                                .Where( x => x != null )
                                .First();
            } catch( Exception ex ) {
                if ( throwOnError ) {
                    throw new ArgumentException( $"{className}が正しく見つかりませんでした", ex );
                }

                return null;
            }

            return type.GetParameterCurveInstance();
        }
        public static IParameterCurve GetParameterCurveInstance( this Type type, bool throwOnError = true )
        {
            try {
                //型がinterfaceを実装しているか確認
                var ifType = type.GetInterface( nameof( IParameterCurve ) );
                if ( ifType == null ) {
                    throw new ArgumentException( $"{type} は、{nameof( IParameterCurve )}を実装しません." );
                }

                // Instance プロパティ情報を取得
                var propInfo = type.GetProperty( "Instance", BindingFlags.Static | BindingFlags.Public );
                if ( propInfo == null ) {
                    throw new ArgumentException( $"{type} は、Instance Propertyを実装しません." );
                }

                //プロパティから値を取得
                var instance = propInfo.GetValue( null ) as IParameterCurve;
                if ( instance == null ) {
                    throw new AggregateException( $"{type}.Instance の戻り値を取得できません." );
                }

                return instance;
            } catch(Exception ex) {
                if ( throwOnError ) {
                    throw new ArgumentException( $"{type}のインスタンスを取得できません", ex );
                }

                return null;
            }
        }

        public static IEnumerable<IParameterCurve> GetParameterCurves()
        {
            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .SelectMany( asm => asm.GetTypes() )
                            .Where( type => type.GetInterface( nameof( IParameterCurve ) ) != null )
                            .Select( type => type.GetParameterCurveInstance( false ) )
                            .Where( x => x != null );
        }
    }
}
