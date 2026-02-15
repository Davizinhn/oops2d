using oops2d.core;
using oops2d.core._internal;
using oops2d.rendering._internal;
using Raylib_cs;
using System.Diagnostics;
using System.Numerics;

namespace oops2d.rendering
{
    public class Sprite2DAnimation
    {
        public string name = "";
        public Texture2D texture;
        string imgPath;
        public int frames = 0;
        public int frameSpeed = 12;
        public bool looped = false;

        public Sprite2DAnimation(string name, string imgPath, int frames, bool looped = false, int frameSpeed = 12)
        {
            if (imgPath == null) return;

            this.imgPath = imgPath;
            this.name = name;
            this.frames = frames;
            this.frameSpeed = frameSpeed;
            this.looped = looped;
        }

        public void LoadSprite()
        {
            texture = Cache.Instance.LoadTexture(imgPath);
        }
    }

    public class Sprite2D : Renderer2D
    {
        public Texture2D texture;
        public bool flipX = false;
        public bool flipY = false;

        public bool Tiled = false;
        public Vector2 TileSize = Vector2.One;

        public List<Sprite2DAnimation> animations = new List<Sprite2DAnimation>();
        Sprite2DAnimation? curAnimation;
        int curAnimationFrame = 0;

        public Sprite2D(string imgPath, Vector2 pos, float rot = 0, float scale = 1, Origin2D origin = Origin2D.Center, bool tiled = false, Vector2 tileSize = default)
        {
            this.transform.Rotation = rot;
            this.transform.Scale = scale;
            this.transform.Position = pos;
            this.Tiled = tiled;
            this.TileSize = tileSize;
            this.origin = origin;

            ColorTint = Color.White;

            LoadSprite(imgPath);
        }

        public override void Draw(Scene2D scene)
        {
            if (texture.Height == 0 || texture.Width == 0) return;

            Rectangle rect;
            if (Tiled) {
                rect = new Rectangle(0, 0, TileSize.X, TileSize.Y);
                utils.Rendering.DrawTextureTiled(texture, rect, new Rectangle(GlobalPosition, new Vector2(rect.Width, rect.Height)), transform.Origin, transform.Rotation, GlobalScale, ColorTint);
            } else {
                if (curAnimation == null)
                {
                    rect = new Rectangle(0, 0, (float)texture.Width * GlobalScale, (float)texture.Height * GlobalScale);
                } 
                else
                {
                    rect = new Rectangle((flipX ? ((curAnimation.frames-1) - curAnimationFrame) : (curAnimationFrame)) * (curAnimation.texture.Width / curAnimation.frames), 0, (float)curAnimation.texture.Width/curAnimation.frames * GlobalScale, (float)curAnimation.texture.Height * GlobalScale);
                }

                Texture2D resizedTex = curAnimation == null ? texture : curAnimation.texture;
                resizedTex.Width = curAnimation == null ? (int)rect.Width : (int)rect.Width * curAnimation.frames;

                resizedTex.Height = flipY ? -(int)rect.Height : (int)rect.Height;
                resizedTex.Width = flipX ? -resizedTex.Width : resizedTex.Width;

                Raylib.DrawTexturePro(resizedTex, rect, new Rectangle(GlobalPosition, new Vector2(rect.Width, rect.Height)), transform.Origin, transform.Rotation, ColorTint);
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

        public override Rectangle GetRectangle(bool local = false)
        {
            if (texture.Height == 0 || texture.Width == 0) return new Rectangle();

            Rectangle rect;

            if (Tiled) {
                rect = new Rectangle(0, 0, TileSize.X, TileSize.Y);
                return new Rectangle(local ? transform.Position : GlobalPosition, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
            } else {
                rect = new Rectangle(0, 0, (float)texture.Width, (float)texture.Height);
                return new Rectangle((local ? transform.Position : GlobalPosition) - transform.Origin, new Vector2(rect.Width * GlobalScale, rect.Height * GlobalScale));
            }
        }

        public override void Update(Scene2D scene)
        {
            UpdateAnimation();

            base.Update(scene);
        }

        float frameTime = 0;
        void UpdateAnimation()
        {
            if (curAnimation == null) 
            {
                frameTime = 0;
                return; 
            }

            frameTime += Raylib.GetFrameTime();
            float frameRate = 1.0f / curAnimation.frameSpeed;
            if (frameTime >= frameRate)
            {
                frameTime -= frameRate;
                curAnimationFrame++;

                if (curAnimationFrame >= curAnimation.frames)
                {
                    if (!curAnimation.looped)
                    {
                        StopAnimation();
                    } else
                    {
                        curAnimationFrame = 0;
                    }
                }
            }
        }

        public void AddAnimation(Sprite2DAnimation animation)
        {
            if (Tiled)
            {
                // TODO: Add tiled sprite animation support
                Debug.Fail("Tiled sprites doesn't support animations"); 
                return;
            }

            if (animation == null) return;
            if (animations.Find(anim => anim.name == animation.name) != null) return;

            animation.LoadSprite();
            animations.Add(animation);
        }

        public void PlayAnimation(string name)
        {
            if (string.IsNullOrEmpty(name)) return;

            Sprite2DAnimation? nextAnimation = animations.Find(anim => anim.name == name);
            if (nextAnimation == null) return;

            if (curAnimation != null)
            {
                StopAnimation();
            }

            curAnimation = nextAnimation;
        }

        public void StopAnimation()
        {
            if (curAnimation == null) return;

            curAnimation = null;
            curAnimationFrame = 0;
        }
    }

}