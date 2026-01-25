using Raylib_cs;
using System.Numerics;

namespace oops2d.Rendering
{
    public static class Utils
    {
        public static void DrawTextureTiled(Texture2D texture, Rectangle source, Rectangle dest, Vector2 origin, float rotation, float scale, Color tint)
        {
            if (texture.Id <= 0 || scale <= 0.0f) return;
            if (source.Width == 0 || source.Height == 0) return;

            int tileWidth = (int)(source.Width * scale);
            int tileHeight = (int)(source.Height * scale);

            if (dest.Width < tileWidth && dest.Height < tileHeight)
            {
                Raylib.DrawTexturePro(texture,
                    new Rectangle(source.X, source.Y, ((float)dest.Width / tileWidth) * source.Width, ((float)dest.Height / tileHeight) * source.Height),
                    dest,
                    origin,
                    rotation,
                    tint);
            }
            else if (dest.Width <= tileWidth)
            {
                int dy = 0;
                for (; dy + tileHeight < dest.Height; dy += tileHeight)
                {
                    Raylib.DrawTexturePro(texture,
                        new Rectangle(source.X, source.Y, ((float)dest.Width / tileWidth) * source.Width, source.Height),
                        new Rectangle(dest.X, dest.Y + dy, dest.Width, tileHeight),
                        origin,
                        rotation,
                        tint);
                }

                if (dy < dest.Height)
                {
                    Raylib.DrawTexturePro(texture,
                        new Rectangle(source.X, source.Y, ((float)dest.Width / tileWidth) * source.Width, ((float)(dest.Height - dy) / tileHeight) * source.Height),
                        new Rectangle(dest.X, dest.Y + dy, dest.Width, dest.Height - dy),
                        origin,
                        rotation,
                        tint);
                }
            }
            else if (dest.Height <= tileHeight)
            {
                int dx = 0;
                for (; dx + tileWidth < dest.Width; dx += tileWidth)
                {
                    Raylib.DrawTexturePro(texture,
                        new Rectangle(source.X, source.Y, source.Width, ((float)dest.Height / tileHeight) * source.Height),
                        new Rectangle(dest.X + dx, dest.Y, tileWidth, dest.Height),
                        origin,
                        rotation,
                        tint);
                }

                if (dx < dest.Width)
                {
                    Raylib.DrawTexturePro(texture,
                        new Rectangle(source.X, source.Y, ((float)(dest.Width - dx) / tileWidth) * source.Width, ((float)dest.Height / tileHeight) * source.Height),
                        new Rectangle(dest.X + dx, dest.Y, dest.Width - dx, dest.Height),
                        origin,
                        rotation,
                        tint);
                }
            }
            else
            {
                int dx = 0;
                for (; dx + tileWidth < dest.Width; dx += tileWidth)
                {
                    int dy = 0;
                    for (; dy + tileHeight < dest.Height; dy += tileHeight)
                    {
                        Raylib.DrawTexturePro(texture, source,
                            new Rectangle(dest.X + dx, dest.Y + dy, tileWidth, tileHeight),
                            origin, rotation, tint);
                    }

                    if (dy < dest.Height)
                    {
                        Raylib.DrawTexturePro(texture,
                            new Rectangle(source.X, source.Y, source.Width, ((float)(dest.Height - dy) / tileHeight) * source.Height),
                            new Rectangle(dest.X + dx, dest.Y + dy, tileWidth, dest.Height - dy),
                            origin, rotation, tint);
                    }
                }

                if (dx < dest.Width)
                {
                    int dy = 0;
                    for (; dy + tileHeight < dest.Height; dy += tileHeight)
                    {
                        Raylib.DrawTexturePro(texture,
                            new Rectangle(source.X, source.Y, ((float)(dest.Width - dx) / tileWidth) * source.Width, source.Height),
                            new Rectangle(dest.X + dx, dest.Y + dy, dest.Width - dx, tileHeight),
                            origin, rotation, tint);
                    }

                    if (dy < dest.Height)
                    {
                        Raylib.DrawTexturePro(texture,
                            new Rectangle(source.X, source.Y, ((float)(dest.Width - dx) / tileWidth) * source.Width, ((float)(dest.Height - dy) / tileHeight) * source.Height),
                            new Rectangle(dest.X + dx, dest.Y + dy, dest.Width - dx, dest.Height - dy),
                            origin, rotation, tint);
                    }
                }
            }
        }

    }
}
