using Raylib_cs;

namespace oops2d.Core
{
    internal class Scene2D
    {
        private List<Object2D> objects = new List<Object2D>();
        private bool isActive = true;
        public Camera2D camera2D = new Camera2D();

        public Scene2D() { }

        List<Object2D> destroying = new List<Object2D>();
        public virtual void Update() { 
            if (!isActive) { return; }
            foreach (Object2D obj in objects)
            {
                if (obj == null || obj.destroyed) { destroying.Add(obj); continue; }
                obj.Update(this);
            }

            foreach (Object2D obj in destroying)
            {
                objects.Remove(obj);
            }
        }
        public virtual void Start() {
            camera2D.Zoom = 1;
        }
        public virtual void Draw()
        {
            if (!isActive) { return; }
            foreach (Object2D obj in objects)
            {
                if (obj == null) continue;
                if (!obj.Visible) continue;
                if (obj.UIElement) continue;
                if (obj.destroyed) continue;
                obj.Draw(this);
            }
        }

        public virtual void DrawUI()
        {
            if (!isActive) { return; }
            foreach (Object2D obj in objects)
            {
                if (obj == null) continue;
                if (!obj.Visible) continue;
                if (!obj.UIElement) continue;
                obj.Draw(this);
            }
        }

        public virtual void Destroy() {
            foreach (Object2D obj in objects)
            {
                if (obj == null) continue;
                obj.Destroy();
            }
            objects.Clear();
            SetActive(false);
        }

        public virtual void Add(Object2D obj) { 
            if (obj == null) return;
            if (objects.Contains(obj)) return;
            objects.Add(obj);

            obj.Start(this);
        }

        public virtual void Remove(Object2D obj)
        {
            if (obj == null) return;
            if (!objects.Contains(obj)) return;
            objects.Remove(obj);
        }

        public virtual void SetActive(bool isActive)
        {
            this.isActive = isActive;
        }
    }
}
