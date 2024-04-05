using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace engine{
    public class Texture{
        public int ID {get; private set;}
        public int width {get; private set;}
        public int height {get; private set;}

        public Texture(int id, int w, int h){
            ID = id;
            width = w;
            height = h;
        }
    }

    public static class TextureLoader{
        private static Dictionary<string, Texture> cachedTextures = new Dictionary<string, Texture>();

        public static Texture LoadTexture(string path, bool flipY = true, TextureMinFilter minFilter = TextureMinFilter.Linear, TextureMagFilter magFilter = TextureMagFilter.Linear, TextureWrapMode wrapMode = TextureWrapMode.Repeat, bool mipmaps = true){
            if(cachedTextures.ContainsKey(path)){
                return cachedTextures[path];
            }
            
            int tex = GL.GenTexture();
            int w,h;
            GL.BindTexture(TextureTarget.Texture2D, tex);

            StbImage.stbi_set_flip_vertically_on_load(flipY? 1: 0);
            using(Stream s = File.OpenRead(path)){
                ImageResult image = ImageResult.FromStream(s, ColorComponents.RedGreenBlueAlpha);
                w = image.Width;
                h = image.Height;
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, w, h, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapMode);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapR, (int)wrapMode);

            if(mipmaps) GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            Texture t = new Texture(tex, w, h);

            cachedTextures.Add(path, t);

            return t;
        }

        public static void CleanUp(){
            foreach(Texture t in cachedTextures.Values){
                GL.DeleteTexture(t.ID);
            }
        }
    }
}