using oops2d.Core;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace oops2d.Rendering.Text
{
    public class TextFormat
    {
        public string Text = "";
        public int Size;
        public Color TextColor = new Color();
        public Font Font = Raylib.GetFontDefault();

        public TextFormat(string text, int size = 12, Color color = new Color(), Font font = default)
        {
            Text = text;
            Size = size;
            TextColor = color;
            Font = font;
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
