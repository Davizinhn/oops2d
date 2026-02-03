using oops2d.Core;
using oops2d.Core.Internal;
using Raylib_cs;
using System.Diagnostics;
using System.Numerics;

namespace oops2d.Rendering.Text
{
    public class Text2D : Object2D
    {
        public TextFormat format;

        public Text2D(string text, Vector2 position, int size = 12, Color color = new Color(), string fontPath = "")
        {
            format = new TextFormat(text, size, color);
            format.SetFont(fontPath);
            transform.Position = position;
        }

        public Text2D(string text, Vector2 position, int size = 12, Color color = new Color(), Font font = default)
        {
            format = new TextFormat(text, size, color, font);
            transform.Position = position;
        }

        public override void Draw(Scene2D scene)
        {
            Vector2 origin = new Vector2(0,0);
            switch (format.Alignment)
            {
                case TextAlignment.Center:
                    origin.X = Raylib.MeasureTextEx(format.Font, format.Text, format.Size, format.Spacing).X / 2;
                    break;
                case TextAlignment.Right:
                    origin.X = -Raylib.MeasureTextEx(format.Font, format.Text, format.Size, format.Spacing).X;
                    break;
                case TextAlignment.Left:
                    origin.X = Raylib.MeasureTextEx(format.Font, format.Text, format.Size, format.Spacing).X;
                    break;
            }

            Raylib.DrawTextPro(format.Font, format.Text, transform.Position, origin, GlobalRotation, format.Size, format.Spacing, format.TextColor);

            base.Draw(scene);
        }

        public override void Destroy(bool? unload = true)
        {
            if (unload == true)
            {
                Cache.Instance.UnloadFont(this.format.Font);
            }

            base.Destroy(unload);
        }
    }
}
