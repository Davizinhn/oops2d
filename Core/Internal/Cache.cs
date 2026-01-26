using Raylib_cs;

namespace oops2d.Core.Internal
{
    internal class Cache
    {
        public static Cache Instance { get; private set; }

        private Dictionary<string, TextureCache> textures = new();
        private Dictionary<string, ImageCache> images = new();

        public Cache() {
            Instance = this;
        }

        public Image LoadImage(string path)
        {
            if (images.TryGetValue(path, out ImageCache img))
            {
                return img.image;
            }

            Image loaded = Raylib.LoadImage(path);
            ImageCache cache = new ImageCache(path, loaded);
            images[path] = cache;

            return loaded;
        }

        public Texture2D LoadTexture(string path)
        {
            if (textures.TryGetValue(path, out TextureCache tex))
            {
                if (tex.texture2D.Id != 0)
                {
                    return tex.texture2D;
                }

                textures.Remove(path);
            }

            Image img = LoadImage(path);
            Texture2D loaded = Raylib.LoadTextureFromImage(img);
            TextureCache cache = new TextureCache(path, loaded);
            textures[path] = cache;

            return loaded;
        }

        public void UnloadTexture(string path)
        {
            if (textures.TryGetValue(path, out TextureCache tex))
            {
                if (tex.texture2D.Id != 0) 
                { 
                    Raylib.UnloadTexture(tex.texture2D);
                }
                textures.Remove(path);
            }
        }

        public void UnloadTexture(Texture2D texture)
        {
            foreach (var kvp in textures)
            {
                if (kvp.Value.texture2D.Id == texture.Id)
                {
                    Raylib.UnloadTexture(kvp.Value.texture2D);
                    textures.Remove(kvp.Key);
                    break;
                }
            }
        }

        public void UnloadImage(string path)
        {
            if (images.TryGetValue(path, out ImageCache img))
            {
                Raylib.UnloadImage(img.image);
                images.Remove(path);
            }
        }

        public void UnloadAll()
        {
            foreach (var tex in textures.Values)
            {
                if (tex.texture2D.Id != 0)
                {
                    Raylib.UnloadTexture(tex.texture2D);
                }

            }

            foreach (var img in images.Values)
            {
                Raylib.UnloadImage(img.image);
            }

            textures.Clear();
            images.Clear();
        }
    }
}

class ImageCache
{
    public string path;
    public Image image;

    public ImageCache(string path, Image image)
    {
        this.path = path;
        this.image = image;
    }
}

class TextureCache
{
    public string path;
    public Texture2D texture2D;

    public TextureCache(string path, Texture2D texture2D)
    {
        this.path = path;
        this.texture2D = texture2D;
    }
}