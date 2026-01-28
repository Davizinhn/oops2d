using Raylib_cs;

namespace oops2d.Rendering.Text
{
    public class TextFormat
    {
        public string Text = "";
        public int Size;
        public Color TextColor = new Color();
        public Font Font = Raylib.GetFontDefault();
        public float Spacing = 1.0f;
        public TextAlignment Alignment = TextAlignment.Left;

        public TextFormat(string text, int size = 12, Color color = new Color(), Font font = default, float spacing = 1, TextAlignment alignment = TextAlignment.Left)
        {
            Text = text;
            Size = size;
            TextColor = color;
            Font = font;
            Spacing = spacing;
            Alignment = alignment;
        }

        public void SetFont(string fontPath)
        {
            if (string.IsNullOrEmpty(fontPath))
            {
                return;
            }

            Font = Raylib.LoadFont(fontPath);
        }
    }
}
