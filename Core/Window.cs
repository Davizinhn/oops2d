using oops2d.Core.Internal;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oops2d.Core
{
    public class Window
    {
        public int Width;
        public int Height;
        public string Name;
        public Image Icon;

        public Window(int width, int height, string name, string iconPath = "")
        {
            Width = width;
            Height = height;
            Name = name;

            if (String.IsNullOrEmpty(iconPath)) return;
            Icon = Raylib.LoadImage(iconPath);
        }
    }
}
