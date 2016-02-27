namespace OpenTKPlatformer
{
    public class ContentPipe
    {
        public static Texture2D LoadTexture(string textureFullPath)
        {
            var texture = new Texture2D(textureFullPath);

            texture.Upload();

            return texture;
        }
    }
}