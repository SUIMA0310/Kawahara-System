using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using DesktopApp.Models;
using DesktopApp.Overlay.Draw.Helpers;
using DesktopApp.Overlay.Draw.Models;

using SharpDX.Direct2D1;

namespace DesktopApp.Overlay.Draw.Views
{
    public class ReactionControl : D2dControl.D2dControl
    {
        #region static field

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(
                "Target",
                typeof(IReactionInteraction),
                typeof(ReactionControl),
                new PropertyMetadata((sender, eventArgs) =>
                {
                    if (sender is ReactionControl reactionControl) {
                        if (eventArgs.OldValue is IReactionInteraction oldValue) {
                            oldValue.Interaction -= reactionControl.Interaction;
                        }

                        if (eventArgs.NewValue is IReactionInteraction newValue) {
                            newValue.Interaction += reactionControl.Interaction;
                        }
                    }
                }));

        // Using a DependencyProperty as the backing store for MaxOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxOpacityProperty =
            DependencyProperty.Register(
                "MaxOpacity",
                typeof(float),
                typeof(ReactionControl),
                new PropertyMetadata(
                    1.0f,
                    null,
                    (sender, value) => ((float)value).CutOut()));

        // Using a DependencyProperty as the backing store for Scale.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(
                "Scale",
                typeof(float),
                typeof(ReactionControl),
                new PropertyMetadata(1.0f));

        // Using a DependencyProperty as the backing store for ShowTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayTimeProperty =
            DependencyProperty.Register(
                "DisplayTime",
                typeof(double),
                typeof(ReactionControl),
                new PropertyMetadata(2.0));

        // Using a DependencyProperty as the backing store for MoveMethod.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoveMethodProperty =
            DependencyProperty.Register(
                "MoveMethod",
                typeof(IParameterCurve),
                typeof(ReactionControl),
                new PropertyMetadata(Constant.Instance));


        // Using a DependencyProperty as the backing store for OpacityCurve.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpacityCurveProperty =
            DependencyProperty.Register(
                "OpacityCurve", 
                typeof(IParameterCurve), 
                typeof(ReactionControl), 
                new PropertyMetadata(Constant.Instance));



        #endregion static field

        #region Properties

        /// <summary>
        /// Controlに表示を追加するためのEvent発火元
        /// </summary>
        public IReactionInteraction Target
        {
            get { return (IReactionInteraction)GetValue( TargetProperty ); }
            set { SetValue( TargetProperty, value ); }
        }

        /// <summary>
        /// 最大不透明度
        /// </summary>
        public float MaxOpacity
        {
            get { return (float)GetValue( MaxOpacityProperty ); }
            set { SetValue( MaxOpacityProperty, value ); }
        }

        /// <summary>
        /// 表示スケール
        /// </summary>
        public float Scale
        {
            get { return (float)GetValue( ScaleProperty ); }
            set { SetValue( ScaleProperty, value ); }
        }

        /// <summary>
        /// 表示時間
        /// </summary>
        public double DisplayTime
        {
            get { return (double)GetValue( DisplayTimeProperty ); }
            set { SetValue( DisplayTimeProperty, value ); }
        }

        /// <summary>
        /// 移動速度計算用メソッド
        /// </summary>
        public IParameterCurve MoveMethod
        {
            get { return (IParameterCurve)GetValue( MoveMethodProperty ); }
            set { SetValue( MoveMethodProperty, value ); }
        }

        /// <summary>
        /// 不透明度計算用メソッド
        /// </summary>
        public IParameterCurve OpacityCurve
        {
            get { return (IParameterCurve)GetValue( OpacityCurveProperty ); }
            set { SetValue( OpacityCurveProperty, value ); }
        }

        #endregion Properties

        private Queue<Item> ViewDates;

        public ReactionControl()
        {
            this.ViewDates = new Queue<Item>();
            this.resCache.Add( "Good", target => new Objects.GoodObject( target ) );
            this.resCache.Add( "Nice", target => new Objects.NiceObject( target ) );
            this.resCache.Add( "Fun", target => new Objects.FunObject( target ) );
        }

        public override void Render( RenderTarget target )
        {
            target.Clear( null );

            target.Transform = Matrix3x2Helper.Identity;
            target.AntialiasMode = AntialiasMode.PerPrimitive;

            //有効表示時間を取得
            var st = TimeSpan.FromSeconds(this.DisplayTime);

            //描画用のObjectを取得
            var good = this.resCache["Good"] as Objects.ObjectBase;
            var nice = this.resCache["Nice"] as Objects.ObjectBase;
            var fun  = this.resCache["Fun"]  as Objects.ObjectBase;

            lock ( this.ViewDates ) {
                foreach ( var item in this.ViewDates ) {
                    var transform = Matrix3x2Helper.Identity;

                    //経過時間の割合を取得
                    float t = item.StartTime.Elapsed(st);

                    //描画位置を指定
                    transform = transform.Translation( item.Animation.Point( this.MoveMethod.GetValue( t ) ) );

                    //表示スケールを指定
                    transform = transform.Scale( this.Scale );

                    //表示色を指定
                    var color = item.Color;

                    //透明度を設定
                    color.A = (1.0f - this.OpacityCurve.GetValue(t)) * this.MaxOpacity;

                    switch ( item.ReactionType ) {
                        case eReactionType.Good:
                            good.Render( transform, color );
                            break;

                        case eReactionType.Nice:
                            nice.Render( transform, color );
                            break;

                        case eReactionType.Fun:
                            fun.Render( transform, color );
                            break;
                    }
                }

                while (
                    this.ViewDates.Any() &&
                    this.ViewDates.Peek().StartTime.Elapsed( st ) >= 1.0 ) {
                    this.ViewDates.Dequeue();
                }
            }
        }

        private void Interaction( eReactionType reactionType, Color color )
        {
            float scale = this.Dispatcher.Invoke( () => this.Scale );

            var item = new Item(this.CreateBezier(scale), reactionType, color);

            lock ( this.ViewDates ) {
                this.ViewDates.Enqueue( item );
            }
        }

        private Bezier CreateBezier( float scale )
        {
            var ret = new Bezier();

            ret.StartPoint.Y = (float)this.ActualHeight - 120.0f * scale;
            ret.Point1.Y = (float)(this.ActualHeight * (2.0 / 3.0));
            ret.Point2.Y = (float)(this.ActualHeight * (1.0 / 3.0));
            ret.EndPoint.Y = 120.0f * scale;

            var random = new Random();
            float center = (float)this.ActualWidth / 2.0f;
            int max = Math.Max((int)(this.ActualWidth - 100.0f * scale), 101);
            int min = (int)(100.0f * scale);

            ret.StartPoint.X = center;
            ret.Point1.X = random.Next( min, max );
            ret.Point2.X = random.Next( min, max );
            ret.EndPoint.X = random.Next( min, max );

            return ret;
        }
    }
}