using System;

using DesktopApp.Models;

using SharpDX.Mathematics.Interop;

namespace DesktopApp.Overlay.Draw.Models
{
    internal struct Item
    {
        public Bezier Animation { get; }
        public eReactionType ReactionType { get; }
        public RawColor4 Color { get; }
        public DateTime StartTime { get; }

        public Item( Bezier animation, eReactionType reactionType, Color color ) : this()
        {
            this.Animation = animation;
            this.ReactionType = reactionType;
            this.Color = new RawColor4( color.Red / 255f, color.Green / 255f, color.Blue / 255f, 1.0f );
            this.StartTime = DateTime.Now;
        }
    }
}