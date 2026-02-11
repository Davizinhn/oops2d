using oops2d.core;
using oops2d.rendering._internal;
using Raylib_cs;
using System.Numerics;

namespace oops2d.physics
{
    // WARNING: This class is a placeholder and does not represent a full-featured physics engine.
    public class Collider2D : Component2D
    {
        public bool IsTrigger = false;
        public Hitbox hitbox = new Hitbox();

        List<Collider2D> colliding = new List<Collider2D>();

        public Action<Collider2D>? OnCollisionEnter = new Action<Collider2D>((Collider2D other) => {});
        public Action<Collider2D>? OnCollisionExit = new Action<Collider2D>((Collider2D other) => {});
        public Action<Collider2D>? OnCollisionStay = new Action<Collider2D>((Collider2D other) => {});

        public override void Start(Scene2D scene)
        {
            UpdateBounds();

            base.Start(scene);
        }

        public override void Update(Scene2D scene)
        {
            hitbox.Bounds.Position = (parent as Object2D)!.GlobalPosition;
            CollisionCheck();

            base.Update(scene);
        }

        public override void Draw(Scene2D scene)
        {
            if (hitbox.Visible)
            {
                Raylib.DrawRectangleLinesEx(GetHitbox(), 3, Color.Green);
            }

            base.Draw(scene);
        }

        public void UpdateBounds()
        {
            Renderer2D renderer = (parent as Renderer2D)!;
            if (renderer != null)
            {
                Rectangle rectangle = renderer.GetRectangle();
                hitbox.Size = new Vector2(rectangle.Width, rectangle.Height);
                hitbox.Bounds = renderer.GetRectangle();
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

                if (Raylib.CheckCollisionRecs(GetHitbox(), otherCollider.GetHitbox()))
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

                if (!Raylib.CheckCollisionRecs(GetHitbox(), otherCollider.GetHitbox()))
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

        public void SetBounds(Rectangle rect)
        {
            hitbox.Bounds = rect;
        }

        public Rectangle GetBounds()
        {
            return hitbox.Bounds;
        }

        public Rectangle GetHitbox()
        {

            return new Rectangle(
                hitbox.Bounds.X + hitbox.GetOrigin().X + (hitbox.Offset.X),
                hitbox.Bounds.Y + hitbox.GetOrigin().Y + (hitbox.Offset.Y),
                hitbox.Size.X == 0 ? hitbox.Bounds.Width : hitbox.Size.X,
                hitbox.Size.Y == 0 ? hitbox.Bounds.Height : hitbox.Size.Y
            );
        }

        public List<Collider2D> GetCollisions()
        {
            return colliding;
        }
    }
}
