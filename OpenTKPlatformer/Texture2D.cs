using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace OpenTKPlatformer
{
    public class Texture2D
    {
        private readonly Bitmap _bitmap;

        public int Height { get; set; }

        public int TextureId { get; set; } = -1;

        public int Width { get; set; }

        public Texture2D(string fullPath)
                : this(new Bitmap(fullPath)) { }

        public Texture2D(Bitmap bitmap)
        {
            _bitmap = bitmap;

            Width = _bitmap.Width;
            Height = _bitmap.Height;
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureId);
        }

        public void Upload()
        {
            TextureId = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, TextureId);

            var bitmapData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
        }
    }
}