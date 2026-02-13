using oops2d.core;
using oops2d.rendering;
using oops2d.rendering._internal;
using oops2d.rendering.text;
using oops2d.utils;
using Raylib_cs;
using System.Numerics;

namespace oops2d.rendering.ui
{
    public class Button2D : Object2D
    {
        public bool active = true;
        public Sprite2D sprite;
        public Text2D label;

        bool hovered = false;
        bool clicked = false;
        public Action<Button2D> onExit;
        public Action<Button2D> onHover;
        public Action<Button2D> onPressed;

        public Button2D(string text, string imgPath, Vector2 position, Action<Button2D> onPressed = null, int textSize = 12)
        {
            sprite = new Sprite2D(imgPath, Vector2.Zero, 0, 1, Origin2D.TopLeft);
            Add(sprite);

            Rectangle localBounds = new Rectangle(Vector2.Zero, new Vector2(sprite.texture.Width, sprite.texture.Height));

            label = new Text2D(text, localBounds, textSize, Color.Black, "");
            label.format.Alignment = TextAlignment.Center;
            Add(label);

            this.onPressed = onPressed;
            UIElement = true;

            transform.Position = position;
        }

        public override void Update(Scene2D scene)
        {
            if (!active || !Visible)
            {
                hovered = false;
                clicked = false;
                return;
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && hovered && !clicked)
            {
                clicked = true;
                onPressed?.Invoke(this);

                new O2Timer(0.2f, (O2Timer timer) =>
                {
                    clicked = false;
                }).Start();

                return;
            }
            
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), sprite.GetRectangle()) && !hovered)
            {
                hovered = true;
                onHover?.Invoke(this);
                return;
            }

            if (!Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), sprite.GetRectangle()) && hovered)
            {
                hovered = false;
                onExit?.Invoke(this);
                return;
            }

            base.Update(scene);
        }
    }
}
