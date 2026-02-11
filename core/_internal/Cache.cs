using Raylib_cs;

namespace oops2d.core._internal
{
    internal class Cache
    {
        public static Cache Instance { get; private set; }

        // TODO: create a var that stores all the diferent types of cache
        private Dictionary<string, TextureCache> textures = new();
        private Dictionary<string, ImageCache> images = new();
        private Dictionary<string, SoundCache> sounds = new();
        private Dictionary<string, MusicCache> musics = new();
        private Dictionary<string, FontCache> fonts = new();

        public Cache() {
            Instance = this;
        }

        public Image LoadImage(string path)
        {
            if (images.TryGetValue(path, out var img))
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
            if (textures.TryGetValue(path, out var tex))
            {
                if (tex.texture2D.Id != 0)
                {
                    return tex.texture2D;
                }

                textures.Remove(path);
            }

            Image img = LoadImage(path);
            Texture2D loaded = Raylib.LoadTextureFromImage(img);
            UnloadImage(path);
            TextureCache cache = new TextureCache(path, loaded);
            textures[path] = cache;

            return loaded;
        }

        public Sound LoadSound(string path)
        {
            if (sounds.TryGetValue(path, out var sound))
            {
                return sound.data;
            }

            Sound loaded = Raylib.LoadSound(path);
            SoundCache cache = new SoundCache(path, loaded);
            sounds[path] = cache;

            return loaded;
        }

        public Music LoadMusic(string path)
        {
            if (musics.TryGetValue(path, out var music))
            {
                return music.data;
            }

            Music loaded = Raylib.LoadMusicStream(path);
            MusicCache cache = new MusicCache(path, loaded);
            musics[path] = cache;

            return loaded;
        }

        public Font LoadFont(string path)
        {
            if (fonts.TryGetValue(path, out var font))
            {
                return font.data;
            }

            Font loaded = Raylib.LoadFont(path);
            FontCache cache = new FontCache(path, loaded);
            fonts[path] = cache;

            return loaded;
        }

        public void UnloadTexture(string path)
        {
            if (textures.TryGetValue(path, out var tex))
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
                    UnloadTexture(kvp.Key);
                    break;
                }
            }
        }

        public void UnloadImage(string path)
        {
            if (images.TryGetValue(path, out var img))
            {
                Raylib.UnloadImage(img.image);
                images.Remove(path);
            }
        }

        public void UnloadSound(string path)
        {
            if (sounds.TryGetValue(path, out var sound))
            {
                Raylib.UnloadSound(sound.data);
                sounds.Remove(path);
            }
        }

        public void UnloadSound(Sound instance)
        {
            foreach (var kvp in sounds)
            {
                if (kvp.Value.data.Stream.Equals(instance.Stream) && kvp.Value.data.FrameCount == instance.FrameCount)
                {
                    UnloadSound(kvp.Key);
                    break;
                }
            }
        }

        public void UnloadMusic(string path)
        {
            if (musics.TryGetValue(path, out var music))
            {
                Raylib.UnloadMusicStream(music.data);
                musics.Remove(path);
            }
        }

        public void UnloadMusic(Music instance)
        {
            foreach (var kvp in musics)
            {
                if (kvp.Value.data.Stream.Equals(instance.Stream) && kvp.Value.data.FrameCount == instance.FrameCount)
                {
                    UnloadMusic(kvp.Key);
                    break;
                }
            }
        }

        public void UnloadFont(string path)
        {
            if (fonts.TryGetValue(path, out var font))
            {
                Raylib.UnloadFont(font.data);
                fonts.Remove(path);
            }
        }

        public void UnloadFont(Font font)
        {
            foreach (var kvp in fonts)
            {
                if (kvp.Value.data.Texture.Equals(font.Texture))
                {
                    UnloadFont(kvp.Key);
                    break;
                }
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

            foreach (var sound in sounds.Values)
            {
                Raylib.UnloadSound(sound.data);
            }

            foreach (var music in musics.Values)
            {
                Raylib.UnloadMusicStream(music.data);
            }

            foreach (var font in fonts.Values)
            {
                Raylib.UnloadFont(font.data);
            }

            textures.Clear();
            images.Clear();
            sounds.Clear();
            musics.Clear();
            fonts.Clear();
        }
    }
}

class CacheReference
{
    public string path = "";
}

class ImageCache : CacheReference
{
    public Image image;

    public ImageCache(string path, Image image)
    {
        this.path = path;
        this.image = image;
    }
}

class TextureCache : CacheReference
{
    public Texture2D texture2D;

    public TextureCache(string path, Texture2D texture2D)
    {
        this.path = path;
        this.texture2D = texture2D;
    }
}

class SoundCache : CacheReference
{
    public Sound data;

    public SoundCache(string path, Sound sound)
    {
        this.path = path;
        this.data = sound;
    }
}

class MusicCache : CacheReference
{
    public Music data;

    public MusicCache(string path, Music music)
    {
        this.path = path;
        this.data = music;
    }
}

class FontCache : CacheReference
{
    public Font data;

    public FontCache(string path, Font font)
    {
        this.path = path;
        this.data = font;
    }
}