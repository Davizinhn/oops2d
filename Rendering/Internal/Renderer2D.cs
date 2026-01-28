using oops2d.Core;
using Raylib_cs;
using System.Numerics;

namespace oops2d.Rendering.Internal
{
    public enum Origin2D
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public class Renderer2D : Object2D
    {
        public Color ColorTint;
        public float Alpha = 1;
        public Origin2D origin = Origin2D.Center;

        public virtual Rectangle GetRectangle()
        {
            return new Rectangle();
        }

        public virtual void SetOrigin(Origin2D origin)
        {
            this.origin = origin;
            switch (origin)
            {
                case Origin2D.TopLeft:
                    transform.Origin = new Vector2(0, 0);
                    break;
                case Origin2D.TopRight:
                    transform.Origin = new Vector2(GetRectangle().Width, 0);
                    break;
                case Origin2D.TopCenter:
                    transform.Origin = new Vector2(GetRectangle().Width / 2, 0);
                    break;
                case Origin2D.Center:
                    transform.Origin = new Vector2(GetRectangle().Width/2, GetRectangle().Height /2);
                    break;
                case Origin2D.CenterLeft:
                    transform.Origin = new Vector2(0, GetRectangle().Height / 2);
                    break;
                case Origin2D.CenterRight:
                    transform.Origin = new Vector2(GetRectangle().Width, GetRectangle().Height / 2);
                    break;
                case Origin2D.BottomLeft:
                    transform.Origin = new Vector2(0, GetRectangle().Height);
                    break;
                case Origin2D.BottomRight:
                    transform.Origin = new Vector2(GetRectangle().Width, GetRectangle().Height);
                    break;
                case Origin2D.BottomCenter:
                    transform.Origin = new Vector2(GetRectangle().Width/2, GetRectangle().Height);
                    break;
            }
        }

        public virtual Origin2D GetOrigin()
        {
            return origin;
        }
    }
}
