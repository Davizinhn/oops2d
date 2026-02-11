using Raylib_cs;

namespace oops2d.core
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
