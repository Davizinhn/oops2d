using System.Numerics;

namespace oops2d.Core
{
    public class Object2D
    {
        public bool Visible = true;
        public bool UIElement = false;

        public Vector2 GlobalPosition = new Vector2();
        public float GlobalRotation = 0;
        public float GlobalScale = 1;

        public List<Object2D> children = new List<Object2D>();
        public bool destroyed = false;

        public Scene2D Scene;
        public Object2D? Parent {get; private set;}

        public Object2D() { }
        public virtual void Update(Scene2D scene) {
            foreach (Object2D obj in children)
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
            foreach (Object2D obj in children)
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
            foreach (Object2D obj in children)
            {
                if (obj == null) continue;
                if (!obj.Visible) continue;
                if (!obj.UIElement) continue;
                obj.Draw(scene);
            }
        }
        public virtual void Destroy(bool ?unloadTexture = true)
        {
            foreach (Object2D obj in children)
            {
                if (obj == null) continue;
                obj.Remove(obj);
                obj.Destroy();
            }
            children.Clear();
            destroyed = true;
        }

        public virtual void Add(Object2D obj)
        {
            if (obj == null) return;
            if (children.Contains(obj)) return;
            obj.Parent = this;
            children.Add(obj);
        }

        public virtual void Remove(Object2D obj)
        {
            if (obj == null) return;
            if (!children.Contains(obj)) return;
            obj.Parent = null;
            children.Remove(obj);
        }
    }
}
