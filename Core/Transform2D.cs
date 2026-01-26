using System.Numerics;

namespace oops2d.Core
{
    public class Transform2D
    {
        public Vector2 Position = new Vector2();
        public float Rotation = 0;
        public float Scale = 1;

        public Vector2 Origin = new Vector2();

        public Transform2D() { }
    }
}
