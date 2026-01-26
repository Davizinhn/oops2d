using oops2d.Core;
using Raylib_cs;
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
            Raylib.DrawTextPro(format.Font, format.Text, transform.Position, new Vector2(0, 0), 0, format.Size, 1, format.TextColor);
            base.Draw(scene);
        }
    }
}
