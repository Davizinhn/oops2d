using oops2d.core;
using Raylib_cs;
using System.Numerics;

namespace oops2d.physics
{
    public enum BodyType2D
    {
        Static,
        Dynamic,
        Kinematic
    }

    // WARNING: This class is a placeholder and does not represent a full-featured physics engine.
    public class Rigidbody2D : Component2D
    {
        public float GravityScale = 9.81f;
        public float Mass = 1.0f;
        public float Drag = 0.1f;
        public Vector2 Velocity = new Vector2(0, 0);
        public bool UseGravity = true;
        public BodyType2D Type = BodyType2D.Dynamic;

        public bool IsGrounded { get; private set; }

        Collider2D? collider;

        public override void Update(Scene2D scene)
        {
            if (parent == null) return;
            if (Type == BodyType2D.Static) return;

            ApplyGravity();
            ApplyDrag();

            base.Update(scene);
        }

        public void AddForce(Vector2 force)
        {
            Velocity += force / Mass;
        }

        void ApplyGravity()
        {
            if (!UseGravity) return;
            if (IsGrounded)
            {
                Velocity = new Vector2(0,0);
                return;
            }
            Velocity += (new Vector2(0, GravityScale) * Mass) * Raylib.GetFrameTime();
        }

        void ApplyDrag()
        {
            if (Type == BodyType2D.Kinematic) return;
            Velocity -= Velocity * Drag * Raylib.GetFrameTime();
        }

        public override void LateUpdate(Scene2D scene)
        {
            if (collider == null)
            {
                collider = parent!.GetComponent<Collider2D>()!;
                return;
            }

            Vector2 currentPosition = parent!.transform.Position;
            Vector2 movement = Velocity * Raylib.GetFrameTime();
            Vector2 finalPosition = currentPosition;
            bool _grounded = false;

            Rectangle myBounds = collider.GetHitbox();
            float myWidth = myBounds.Width;
            float myHeight = myBounds.Height;

            List<Collider2D> otherColliders = collider.GetCollisions();

            if (movement.X != 0)
            {
                finalPosition.X += movement.X;

                foreach (Collider2D? otherCollider in otherColliders)
                {
                    if (!ShouldCollideWith(otherCollider)) continue;

                    Rectangle otherRect = otherCollider!.GetHitbox();
                    Rectangle testRect = new Rectangle(finalPosition.X-myWidth/2, currentPosition.Y-myHeight/2, myWidth, myHeight);

                    if (Raylib.CheckCollisionRecs(testRect, otherRect))
                    {
                        if (movement.X > 0)
                        {
                            finalPosition.X = otherRect.X - myWidth / 2;
                        }
                        else
                        {
                            finalPosition.X = otherRect.X + otherRect.Width / 2;
                        }
                        Velocity.X = 0;
                        break;
                    }
                }
            }

            if (movement.Y != 0)
            {
                finalPosition.Y += movement.Y;

                foreach (Collider2D? otherCollider in otherColliders)
                {
                    if (!ShouldCollideWith(otherCollider)) continue;

                    Rectangle otherRect = otherCollider!.GetHitbox();
                    Rectangle testRect = new Rectangle(finalPosition.X - myWidth / 2, finalPosition.Y - myHeight / 2, myWidth, myHeight);

                    if (Raylib.CheckCollisionRecs(testRect, otherRect))
                    {
                        if (movement.Y > 0)
                        {
                            finalPosition.Y = otherRect.Y - myHeight / 2;
                            _grounded = true;
                        }
                        else
                        {
                            finalPosition.Y = otherRect.Y + otherRect.Height / 2;
                        }
                        Velocity.Y = 0;
                        break;
                    }
                }
            }

            IsGrounded = _grounded;
            parent!.transform.Position = finalPosition;

            base.LateUpdate(scene);
        }

        bool ShouldCollideWith(Collider2D? otherCollider)
        {
            if (otherCollider == null) return false;
            if (otherCollider == collider) return false;
            if (!otherCollider.enabled) return false;
            if (collider!.IsTrigger) return false;
            if (otherCollider.IsTrigger) return false;

            Rectangle otherRect = otherCollider.GetHitbox();
            float distance = Vector2.Distance(new Vector2(otherRect.X, otherRect.Y), collider.GetHitbox().Position);
            if (distance > 500) return false;

            return true;
        }
    }
}
