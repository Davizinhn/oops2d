using Raylib_cs;

namespace oops2d.Core.Generics
{
    internal class Cache
    {
        public static List<TextureCache> textures = new List<TextureCache>();
        public static List<ImageCache> images = new List<ImageCache>();

        public static Cache Instance { get; private set; }

        Image blankImage = new Image();
        Texture2D blankTex = new Texture2D();
        
        public Cache() {
            Instance = this;
        }

        public Image LoadImage(string path)
        {
            Image found = FindImageInCache(path);

            if (found.Equals(blankImage))
            {
                found = Raylib.LoadImage(path);
                images.Add(new ImageCache(path, found));
            }

            return found;
        }

        public Texture2D LoadTexture(string path)
        {
            Texture2D found = FindTextureInCache(path);

            if (found.Equals(blankTex))
            {
                Image image = LoadImage(path);

                found = Raylib.LoadTextureFromImage(image);
                textures.Add(new TextureCache(path, found));
            }

            return found;
        }

        Image FindImageInCache(string path)
        {
            foreach (ImageCache imageCache in images)
            {
                if (imageCache.path == path)
                {
                    return imageCache.image;
                }
            }


            return blankImage;
        }

        Texture2D FindTextureInCache(string path)
        {
            foreach (TextureCache textureCache in textures)
            {
                if (textureCache.path == path)
                {
                    return textureCache.texture2D;
                }
            }


            return blankTex;
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