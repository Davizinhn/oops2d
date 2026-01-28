using oops2d.Core;
using oops2d.Core.Internal;
using oops2d.Rendering.Internal;
using Raylib_cs;
using System.Numerics;

namespace oops2d.Rendering
{
    public class Sprite2D : Renderer2D
    {
        public Texture2D texture;
        public bool Tiled = false;
        public Vector2 TileSize = Vector2.One;

        public Sprite2D(string imgPath, Vector2 pos, float rot = 0, float scale = 1, Color tint = new Color(), Origin2D origin = Origin2D.Center, bool tiled = false, Vector2 tileSize = default)
        {
            this.ColorTint = tint;

            if (tint.ToString() == new Color().ToString())
            {
                ColorTint = Color.White;
            }

            this.transform.Rotation = rot;
            this.transform.Scale = scale;
            this.transform.Position = pos;
            this.Tiled = tiled;
            this.TileSize = tileSize;
            this.origin = origin;

            LoadSprite(imgPath);
        }

        public override void Draw(Scene2D scene)
        {
            if (texture.Height == 0 || texture.Width == 0) return;

            Rectangle rect;
            if (Tiled) {
                rect = new Rectangle(0, 0, TileSize.X, TileSize.Y);
                Utils.DrawTextureTiled(texture, rect, new Rectangle(GlobalPosition, new Vector2(rect.Width, rect.Height)), transform.Origin, transform.Rotation, GlobalScale, new Color(ColorTint.R, ColorTint.B, ColorTint.G, Alpha));
            } else {
                rect = new Rectangle(0, 0, (float)texture.Width * GlobalScale, (float)texture.Height * GlobalScale);
                Texture2D resizedTex = texture;
                resizedTex.Width = (int)rect.Width;
                resizedTex.Height = (int)rect.Height;
                Raylib.DrawTexturePro(resizedTex, rect, new Rectangle(GlobalPosition, new Vector2(rect.Width, rect.Height)), transform.Origin, transform.Rotation, new Color(ColorTint.R, ColorTint.B, ColorTint.G, Alpha));
            }

            base.Draw(scene);
        }

        public virtual void LoadSprite(string imgPath)
        {
            if (imgPath == null) return;

            texture = Cache.Instance.LoadTexture(imgPath);
            this.SetOrigin(origin);
        }

        public override void Destroy(bool ?unloadTexture = false)
        {
            if (unloadTexture!.Value == true)
            {
                Cache.Instance.UnloadTexture(texture);
            }

            base.Destroy();
        }

        public override Rectangle GetRectangle()
        {
            if (texture.Height == 0 || texture.Width == 0) return new Rectangle();

            Rectangle rect;

            if (Tiled) {
                rect = new Rectangle(0, 0, TileSize.X, TileSize.Y);
                return new Rectangle(GlobalPosition, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
            } else {
                rect = new Rectangle(0, 0, (float)texture.Width, (float)texture.Height);
                return new Rectangle(GlobalPosition - transform.Origin, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
            }
        }
    }

}