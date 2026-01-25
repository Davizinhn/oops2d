using Raylib_cs;
using System.Numerics;
using oops2d.Core.Internal;
using oops2d.Core;

namespace oops2d.Rendering
{
    public class Sprite2D : Transform2D
    {
        public Texture2D texture;
        public Color ColorTint;
        public bool Tiled = false;
        public Vector2 TileSize = Vector2.One;

        public Sprite2D(string imgPath, Vector2 pos, float rot = 0, float scale = 1, Color tint = new Color(), bool tiled = false, Vector2 tileSize = default)
        {
            this.ColorTint = tint;

            if (tint.ToString() == new Color().ToString())
            {
                ColorTint = Color.White;
            }

            this.Rotation = rot;
            this.Scale = scale;
            this.Position = pos;
            this.Tiled = tiled;
            this.TileSize = tileSize;

            LoadSprite(imgPath);
        }

        public override void Draw(Scene2D scene)
        {
            if (texture.Height == 0 || texture.Width == 0) return;

            Rectangle rect;
            if (Tiled) {
                rect = new Rectangle(0, 0, TileSize.X, TileSize.Y);
                Utils.DrawTextureTiled(texture, rect, new Rectangle(GlobalPosition, new Vector2(rect.Width, rect.Height)), Origin, Rotation, GlobalScale, ColorTint);
            } else {
                rect = new Rectangle(0, 0, (float)texture.Width, (float)texture.Height);
                Raylib.DrawTexturePro(texture, rect, new Rectangle(GlobalPosition, new Vector2(rect.Width, rect.Height)), Origin, Rotation, ColorTint);
            }

            base.Draw(scene);
        }

        public virtual void LoadSprite(string imgPath)
        {
            if (imgPath == null) return;

            Rectangle rect;
            if (Tiled)
            {
                rect = new Rectangle(0, 0, TileSize.X, TileSize.Y);
                Origin = new Vector2(rect.Width / 2, rect.Height / 2);
            } else
            {
                rect = new Rectangle(0, 0, (float)texture.Width, (float)texture.Height);
                Origin = new Vector2(rect.Width / 2, rect.Height / 2);
            }


            texture = Cache.Instance.LoadTexture(imgPath);
        }

        public override void Destroy(bool ?unloadTexture = true)
        {
            if (!unloadTexture.HasValue || unloadTexture.Value == true)
            {
                Raylib.UnloadTexture(texture);
            }

            base.Destroy();
        }

        public virtual Rectangle GetRectangle()
        {
            if (texture.Height == 0 || texture.Width == 0) return new Rectangle();

            Rectangle rect;

            if (Tiled) {
                rect = new Rectangle(0, 0, TileSize.X, TileSize.Y);
                return new Rectangle(GlobalPosition, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
            } else {
                rect = new Rectangle(0, 0, (float)texture.Width, (float)texture.Height);
                return new Rectangle(GlobalPosition, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
            }
        }
    }

}