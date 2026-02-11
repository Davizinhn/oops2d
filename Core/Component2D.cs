using oops2d.core._internal;

namespace oops2d.core
{
    public class Component2D : Behaviour
    {
        public Object2D? parent;
        public bool enabled = true;
        public virtual void Destroy() { }
    }
}
