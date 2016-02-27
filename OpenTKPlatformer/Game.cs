using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKPlatformer
{
    public class Game : GameWindow
    {
        public Game(int width, int height)
                : base(width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.ClearColor(Color.CornflowerBlue);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Red);
            GL.Vertex2(0, 0);

            GL.Color3(Color.Blue);
            GL.Vertex2(1, 0);

            GL.Color3(Color.White);
            GL.Vertex2(1, -0.9f);

            GL.Color3(Color.Yellow);
            GL.Vertex2(0, -1);

            GL.End();

            SwapBuffers();
        }
    }
}