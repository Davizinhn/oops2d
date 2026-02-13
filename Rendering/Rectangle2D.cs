using Raylib_cs;
using System.Numerics;
using oops2d.core._internal;
using oops2d.core;
using oops2d.rendering._internal;

namespace oops2d.rendering
{
    public class Rectangle2D : Renderer2D
    {
        public Vector2 Size;

        public Rectangle2D(Color tint, Vector2 pos = default, Vector2 size = default, float rot = 0)
        {
            this.ColorTint = tint;
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

        public override Rectangle GetRectangle(bool local = false)
        {
            Rectangle rect = new Rectangle(local ? transform.Position.X : GlobalPosition.X, local ? transform.Position.Y : GlobalPosition.Y, Size.X * GlobalScale, Size.Y * GlobalScale);
            return new Rectangle((local ? transform.Position : GlobalPosition) - transform.Origin, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
        }
    }

}