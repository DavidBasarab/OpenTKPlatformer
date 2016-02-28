using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKPlatformer
{
    public class Game : GameWindow
    {
        private Texture2D _texture;
        private readonly View _view;

        public Game(int width, int height)
                : base(width, height)
        {
            GL.Enable(EnableCap.Texture2D);

            _view = new View(Vector2.Zero, 1.0, 0.0);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _texture = ContentPipe.LoadTexture(@"F:\Code\OpenTKPlatformer\OpenTKPlatformer\Content\brownTile.jpg");
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.ClearColor(Color.CornflowerBlue);

            SpriteBatch.Begin(Width, Height);

            _view.ApplyTransform();

            _texture.Bind();

            SpriteBatch.Draw(_texture, Vector2.Zero, new Vector2(0.2f, 0.2f), Color.Green, new Vector2(10, 50));
            SpriteBatch.Draw(_texture, new Vector2(100, 10), new Vector2(0.2f, 0.2f), Color.Pink, new Vector2(10, 50));
            SpriteBatch.Draw(_texture, new Vector2(200, 20), new Vector2(0.2f, 0.2f), Color.Yellow, new Vector2(10, 50));
            SpriteBatch.Draw(_texture, new Vector2(300, 30), new Vector2(0.2f, 0.2f), Color.Blue, new Vector2(10, 50));

            //GL.Begin(PrimitiveType.Quads);

            //GL.Color3(Color.Red);
            //GL.TexCoord2(0, 0);
            //GL.Vertex2(0, 0);

            //GL.Color3(Color.Blue);
            //GL.TexCoord2(1, 0);
            //GL.Vertex2(1, 0);

            //GL.Color3(Color.White);
            //GL.TexCoord2(1, 1);
            //GL.Vertex2(1, -0.9f);

            //GL.Color3(Color.Yellow);
            //GL.TexCoord2(0, 1);
            //GL.Vertex2(0, -1);

            //GL.End();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //_view.Position = new Vector2(_view.Position.X, _view.Position.Y + 0.01f);
            //_view.Zoom -= 0.01f;

            _view.Update();
        }
    }
}