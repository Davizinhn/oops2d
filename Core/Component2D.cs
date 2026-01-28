using oops2d.Core.Internal;

namespace oops2d.Core
{
    public class Component2D : Behaviour
    {
        public Object2D? parent;
        public bool enabled = true;

        public override void Start(Scene2D scene) { base.Start(scene); }
        public override void Update(Scene2D scene) { base.Update(scene); }
        public virtual void Destroy() { }
    }
}
