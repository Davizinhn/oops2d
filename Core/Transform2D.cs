using System.Numerics;

namespace oops2d.Core
{
    public class Transform2D : Object2D
    {
        public Vector2 Position = new Vector2();
        public float Rotation = 0;
        public float Scale = 1;

        public Vector2 Origin = new Vector2();

        public Vector2 GlobalPosition
        {
            get
            {
                if (Parent == null) return Position;
                return Parent.GlobalPosition + Position;
            }
        }
        public float GlobalRotation
        {
            get
            {
                if (Parent == null) return Rotation;
                return Parent.GlobalRotation + Rotation;
            }
        }
        public float GlobalScale
        {
            get
            {
                if (Parent == null) return Scale;
                return Parent.GlobalScale * Scale;
            }
        }

        public Transform2D() { }
    }
}
