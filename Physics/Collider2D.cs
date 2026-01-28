using oops2d.Core;
using oops2d.Rendering.Internal;
using Raylib_cs;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace oops2d.Physics
{
    // WARNING: This class is a placeholder and does not represent a full-featured physics engine.
    public class Collider2D : Component2D
    {
        public bool IsTrigger = false;
        Rectangle bound;
        List<Collider2D> colliding = new List<Collider2D>();

        public Action<Collider2D>? OnCollisionEnter = new Action<Collider2D>((Collider2D other) => {});
        public Action<Collider2D>? OnCollisionExit = new Action<Collider2D>((Collider2D other) => {});
        public Action<Collider2D>? OnCollisionStay = new Action<Collider2D>((Collider2D other) => {});

        public override void Update(Scene2D scene)
        {
            UpdateBounds();
            CollisionCheck();

            base.Update(scene);
        }

        void UpdateBounds()
        {
            Renderer2D renderer = (parent as Renderer2D)!;
            if (renderer != null)
            {
                bound = renderer.GetRectangle();
            }
        }

        void CollisionCheck()
        {
            foreach (Collider2D otherCollider in colliding)
            {
                if (otherCollider == null) continue;
                if (!colliding.Contains(otherCollider)) continue;

                OnCollisionStay!(otherCollider);
            }

            foreach (Collider2D? otherCollider in this.parent!.Scene!.GetObjectsByType<Collider2D>())
            {
                if (otherCollider == null) continue;
                if (otherCollider == this) continue;
                if (colliding.Contains(otherCollider)) continue;

                if (Raylib.CheckCollisionRecs(bound, otherCollider.GetBounds()))
                {
                    OnCollisionEnter!(otherCollider);
                    colliding.Add(otherCollider);
                }
            }

            List<Collider2D> toRemove = new List<Collider2D>();
            foreach (Collider2D otherCollider in colliding)
            {
                if (otherCollider == null) continue;
                if (!colliding.Contains(otherCollider)) continue;
                if (otherCollider == this) continue;

                if (!Raylib.CheckCollisionRecs(bound, otherCollider.GetBounds()))
                {
                    OnCollisionExit!(otherCollider);
                    toRemove.Add(otherCollider);
                }
            }

            foreach (Collider2D rem in toRemove)
            {
                colliding.Remove(rem);
            }
        }

        public Rectangle GetBounds()
        {
            return bound;
        }
    }
}
