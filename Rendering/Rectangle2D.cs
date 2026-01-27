using Raylib_cs;
using System.Numerics;
using oops2d.Core.Internal;
using oops2d.Core;

namespace oops2d.Rendering
{
    public class Rectangle2D : Object2D
    {
        public Vector2 Size;
        public Color ColorTint;

        public Rectangle2D(Color tint = new Color(), Vector2 pos = default, Vector2 size = default, float rot = 0)
        {
            this.ColorTint = tint;

            if (tint.ToString() == new Color().ToString())
            {
                ColorTint = Color.White;
            }

            this.transform.Rotation = rot;
            this.transform.Position = pos;
            this.Size = size;
        }

        public override void Draw(Scene2D scene)
        {
            Rectangle rect = new Rectangle(GlobalPosition.X, GlobalPosition.Y, Size.X * GlobalScale, Size.Y * GlobalScale);
            Raylib.DrawRectanglePro(rect, transform.Origin, transform.Rotation, ColorTint);

            base.Draw(scene);
        }

        public virtual Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle(GlobalPosition.X, GlobalPosition.Y, Size.X * GlobalScale, Size.Y * GlobalScale);
            return new Rectangle(GlobalPosition - transform.Origin, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
        }
    }

}