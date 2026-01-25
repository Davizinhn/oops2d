using Raylib_cs;
using System.Numerics;

namespace oops2d.Core
{
    public class Object2D
    {
        public bool Visible = true;
        public bool UIElement = false;
        public Vector2 Position = new Vector2();
        public float Rotation = 0;
        public float Scale = 1;
        List<Object2D> objects = new List<Object2D>();

        public bool destroyed = false;

        public Scene2D Scene;

        public Object2D? Parent {get; private set;}
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

        public Object2D() { }
        public virtual void Update(Scene2D scene) {
            foreach (Object2D obj in objects)
            {
                if (obj == null) continue;
                obj.Update(scene);
            }
        }
        public virtual void Start(Scene2D scene) {
            Scene = scene;
        }
        public virtual void Draw(Scene2D scene)
        {
            if (!Visible) { return; }
            foreach (Object2D obj in objects)
            {
                if (obj == null) continue;
                if (!obj.Visible) continue;
                if (obj.UIElement) continue;
                obj.Draw(scene);
            }
        }

        public virtual void DrawUI(Scene2D scene)
        {
            if (!Visible) { return; }
            foreach (Object2D obj in objects)
            {
                if (obj == null) continue;
                if (!obj.Visible) continue;
                if (!obj.UIElement) continue;
                obj.Draw(scene);
            }
        }
        public virtual void Destroy(bool ?unloadTexture = true)
        {
            foreach (Object2D obj in objects)
            {
                if (obj == null) continue;
                obj.Remove(obj);
                obj.Destroy();
            }
            objects.Clear();
            destroyed = true;
        }

        public virtual void Add(Object2D obj)
        {
            if (obj == null) return;
            if (objects.Contains(obj)) return;
            obj.Parent = this;
            objects.Add(obj);
        }

        public virtual void Remove(Object2D obj)
        {
            if (obj == null) return;
            if (!objects.Contains(obj)) return;
            obj.Parent = null;
            objects.Remove(obj);
        }
    }
}
