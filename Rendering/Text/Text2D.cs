using oops2d.core;
using oops2d.core._internal;
using oops2d.rendering._internal;
using Raylib_cs;
using System.Diagnostics;
using System.Numerics;

namespace oops2d.rendering.text
{
    public class Text2D : Object2D
    {
        public TextFormat format;
        public Rectangle? bounds;
        public bool textWrap = false;

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

        public Text2D(string text, Rectangle bounds, int size = 12, Color color = new Color(), string fontPath = "")
        {
            format = new TextFormat(text, size, color);
            format.SetFont(fontPath);
            this.bounds = bounds;
            textWrap = true;
        }

        public override void Draw(Scene2D scene)
        {
            string displayText = format.Text;
            if (textWrap && bounds.HasValue)
            {
                displayText = WrapText(format.Text, bounds.Value.Width);
            }

            Vector2 textSize = Raylib.MeasureTextEx(format.Font, displayText, format.Size, format.Spacing);
            Vector2 actualPosition = bounds.HasValue ? PositionInBounds(textSize, bounds.Value) : GlobalPosition;
            Vector2 origin = GetOrigin(textSize);

            Raylib.DrawTextPro(format.Font, displayText, actualPosition, origin, GlobalRotation, format.Size, format.Spacing, format.TextColor);

            base.Draw(scene);
        }

        Vector2 GetOrigin(Vector2 textSize)
        {
            switch (format.Alignment)
            {
                case TextAlignment.Center:
                    return new Vector2(textSize.X / 2, 0);
                case TextAlignment.Right:
                    return new Vector2(textSize.X, 0);
                case TextAlignment.Left:
                    return Vector2.Zero;
                default:
                    return Vector2.Zero;
            }
        }

        Vector2 PositionInBounds(Vector2 textSize, Rectangle rect)
        {
            Vector2 result = GlobalPosition;

            // TODO: Add vertical alignment to TextFormat
            switch (format.Alignment)
            {
                case TextAlignment.Center:
                    result.X += rect.X + (rect.Width / 2);
                    break;
                case TextAlignment.Right:
                    result.X += rect.X + rect.Width;
                    break;
                case TextAlignment.Left:
                    result.X += rect.X;
                    break;
            }

            result.Y += (rect.Height - textSize.Y) / 2;

            return result;
        }

        string WrapText(string text, float maxWidth)
        {
            if (string.IsNullOrEmpty(text)) 
            {
                return text;
            }

            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = "";

            foreach (var word in words)
            {
                var line = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                var lineSize = Raylib.MeasureTextEx(format.Font, line, format.Size, format.Spacing);

                if (lineSize.X > maxWidth && !string.IsNullOrEmpty(currentLine))
                {
                    lines.Add(currentLine);
                    currentLine = word;
                } else
                {
                    currentLine = line;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                lines.Add(currentLine);
            }

            return string.Join("\n", lines);
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
