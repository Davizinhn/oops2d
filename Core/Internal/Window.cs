using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oops2d.Core.Internal
{
    public class Window
    {
        public int Width;
        public int Height;
        public string Name;

        public Window(int width, int height, string name)
        {
            Width = width;
            Height = height;
            Name = name;
        }
    }
}
