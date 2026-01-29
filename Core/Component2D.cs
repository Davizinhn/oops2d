using oops2d.Core.Internal;

namespace oops2d.Core
{
    public class Component2D : Behaviour
    {
        public Object2D? parent;
        public bool enabled = true;
        public virtual void Destroy() { }
    }
}
