using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using DesktopApp.Overlay.Draw.Helpers;
using System.Windows;

namespace DesktopApp.Overlay.Draw.Views
{
    public class ReactionControl : D2dControl.D2dControl
    {

        public Models.IReactionInteraction Target
        {
            get { return (Models.IReactionInteraction)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(
                "Target",
                typeof(Models.IReactionInteraction),
                typeof(ReactionControl),
                new PropertyMetadata((sender, eventArgs) =>
                {

                    if (sender is ReactionControl reactionControl) {

                        if (eventArgs.OldValue is Models.IReactionInteraction oldValue) {
                            oldValue.Interaction -= reactionControl.Interaction;
                        }

                        if (eventArgs.NewValue is Models.IReactionInteraction newValue) {
                            newValue.Interaction += reactionControl.Interaction;
                        }

                    }

                }));

        private Queue<Models.Item> ViewDates;

        public ReactionControl()
        {

            this.ViewDates = new Queue<Models.Item>();
            this.resCache.Add("Good", target => new Objects.GoodObject(target));

        }

        public override void Render(RenderTarget target)
        {
            target.Clear(null);

            target.Transform = Matrix3x2Helper.Identity;
            target.AntialiasMode = AntialiasMode.PerPrimitive;


            var good = this.resCache["Good"] as Objects.ObjectBase;

            lock (this.ViewDates) {
                foreach (var item in this.ViewDates) {

                    float t = item.StartTime.Elapsed(TimeSpan.FromSeconds(2));
                    var point = item.Animation.Point(t);
                    var color = item.Color;
                    color.A = 1.0f - t;

                    switch (item.ReactionType) {
                        case DesktopApp.Models.eReactionType.Good:
                            good.Render(point, color);
                            break;
                        case DesktopApp.Models.eReactionType.Nice:
                            //TODO
                            good.Render(point, color);
                            break;
                        case DesktopApp.Models.eReactionType.Fun:
                            //TODO
                            good.Render(point, color);
                            break;
                    }

                }

                while( 
                    this.ViewDates.Any() &&
                    this.ViewDates.Peek().StartTime.Elapsed(TimeSpan.FromSeconds(2)) >= 1.0) {
                    this.ViewDates.Dequeue();
                }

            }

        }

        private void Interaction(DesktopApp.Models.eReactionType reactionType, DesktopApp.Models.Color color)
        {

            var item = new Models.Item(this.CreateBezier(), reactionType, color);

            lock (this.ViewDates) {

                this.ViewDates.Enqueue(item);

            }

        }

        private Models.Bezier CreateBezier()
        {

            var ret = new Models.Bezier();

            ret.StartPoint.Y = (float)this.ActualHeight - 120.0f;
            ret.Point1.Y = (float)(this.ActualHeight * (2.0 / 3.0));
            ret.Point2.Y = (float)(this.ActualHeight * (1.0 / 3.0));
            ret.EndPoint.Y = 120.0f;

            var random = new Random();
            float center = (float)this.ActualWidth / 2.0f;
            int max = Math.Max((int)this.ActualWidth - 100, 101);
            int min = 100;

            ret.StartPoint.X = center;
            ret.Point1.X = random.Next(min, max);
            ret.Point2.X = random.Next(min, max);
            ret.EndPoint.X = random.Next(min, max);

            return ret;

        }

    }
}
