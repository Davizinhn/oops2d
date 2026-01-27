using oops2d.Core;
using Raylib_cs;

namespace oops2d.Rendering.Internal
{
    public class Renderer2D : Object2D
    {
        public Color ColorTint;
        public float Alpha = 1;

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle();
        }
    }
}
