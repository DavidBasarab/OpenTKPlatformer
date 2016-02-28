﻿using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKPlatformer
{
    public class SpriteBatch
    {
        public static void Draw(Texture2D texture, Vector2 position, Vector2 scale, Color color, Vector2 origin)
        {
            var vertices = new Vector2[4]
                           {
                                   new Vector2(0, 0),
                                   new Vector2(1, 0),
                                   new Vector2(1, 1),
                                   new Vector2(0, 1)
                           };

            texture.Bind();

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(color);

            for (var i = 0; i < 4; i++)
            {
                GL.TexCoord2(vertices[i]);

                vertices[i].X *= texture.Width;
                vertices[i].Y *= texture.Height;
                vertices[i] -= origin;
                vertices[i] *= scale;

                vertices[i] += position;

                GL.Vertex2(vertices[i]);
            }

            GL.End();
        }

        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-screenWidth / 2f, screenWidth / 2f, screenHeight / 2f, -screenHeight / 2f, 0f, 1.0f);
        }
    }
}