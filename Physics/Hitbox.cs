using Raylib_cs;
using System.Numerics;

namespace oops2d.Physics
{
    public class Hitbox
    {
        public Rectangle Bounds;
        public bool UseOrigin = true;
        public Vector2 Offset = new Vector2(0, 0);
        public Vector2 Size = new Vector2(0, 0);

        public bool Visible = false;

        public Vector2 GetOrigin()
        {
            if (UseOrigin)
            {
                return -new Vector2(Size.X / 2, Size.Y / 2);
            }
            else
            {
                return new Vector2(0,0);
            }
        }
    }
}
