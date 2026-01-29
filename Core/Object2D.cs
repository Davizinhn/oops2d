using oops2d.Core.Internal;
using oops2d.Physics;
using Raylib_cs;
using System.Numerics;
using static System.Formats.Asn1.AsnWriter;

namespace oops2d.Core
{
    public class Object2D : Behaviour
    {
        public string Tag = "";

        public bool Visible = true;
        public bool UIElement = false;

        public Transform2D transform = new Transform2D();
        public Vector2 GlobalPosition
        {
            get
            {
                if (parent == null) return transform.Position;
                return parent.GlobalPosition + transform.Position;
            }
        }
        public float GlobalRotation
        {
            get
            {
                if (parent == null) return transform.Rotation;
                return parent.GlobalRotation + transform.Rotation;
            }
        }
        public float GlobalScale
        {
            get
            {
                if (parent == null) return transform.Scale;
                return parent.GlobalScale * transform.Scale;
            }
        }

        public List<Object2D> children = new List<Object2D>();

        public bool destroyed = false;

        public Scene2D? Scene;
        public Object2D? parent { get; private set; }

        public Object2D() { 
            Scene = Game.CurrentScene;
        }

        public override void Update(Scene2D scene)
        {
            foreach (Component2D component in components)
            {
                if (!component.enabled) continue;
                component.Update(scene);
            }

            foreach (Object2D obj in children)
            {
                if (obj == null) continue;
                obj.Update(scene);
            }

            base.Update(scene);
        }

        public override void LateUpdate(Scene2D scene)
        {
            foreach (Component2D component in components)
            {
                if (!component.enabled) continue;
                component.LateUpdate(scene);
            }

            foreach (Object2D obj in children)
            {
                if (obj == null) continue;
                obj.LateUpdate(scene);
            }

            base.LateUpdate(scene);
        }

        public override void Start(Scene2D scene)
        {
            Scene = scene;

            base.Start(scene);
        }

        public override void Draw(Scene2D scene)
        {
            if (!Visible) { return; }

            foreach (Object2D obj in children)
            {
                if (obj == null) continue;
                if (!obj.Visible) continue;
                if (obj.UIElement) continue;
                obj.Draw(scene);
            }

            foreach (Component2D component in components)
            {
                if (!component.enabled) continue;
                component.Draw(scene);
            }

            base.Draw(scene);
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
        public virtual void Destroy(bool? unloadTexture = true)
        {
            foreach(Component2D component in components)
            {
                component.Destroy();
            }
            components.Clear();

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

            obj.parent = this;
            children.Add(obj);
        }

        public virtual void Remove(Object2D obj)
        {
            if (obj == null) return;
            if (!children.Contains(obj)) return;

            obj.parent = null;
            children.Remove(obj);
        }

        public List<Component2D> components = new List<Component2D>();
        public T AddComponent<T>() where T : Component2D, new()
        {
            T component = new T();
            component.parent = this;
            components.Add(component);
            component.Start(Scene!);
            return component;
        }

        public void RemoveComponent(Component2D component)
        {
            if (components.Contains(component))
            {
                components.Remove(component);
            }
        }

        public T? GetComponent<T>() where T : Component2D
        {
            foreach (Component2D component in components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }
            return null;
        }

        public T? GetComponentInChildren<T>() where T : Component2D
        {
            foreach (Object2D child in children)
            {
                T? component = child.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }

                T? childComponent = child.GetComponentInChildren<T>();
                if (childComponent != null)
                {
                    return childComponent;
                }
            }
            return null;
        }

        public List<T>? GetComponentsInChildren<T>() where T : Component2D
        {
            List<T> found = new List<T>();

            foreach (Object2D child in children)
            {
                if (child.GetComponent<T>() is T component)
                {
                    found.Add(component);
                }
                List<T>? components = child.GetComponentsInChildren<T>();
                if (components != null)
                {
                    foreach (T comp in components)
                    {
                        if (comp != null)
                        {
                            if (found.Contains(comp)) continue;
                            found.Add(comp);
                        }
                    }
                }
            }
            return found;
        }
    }
}
