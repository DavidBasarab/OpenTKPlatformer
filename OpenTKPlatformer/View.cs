using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKPlatformer
{
    public class View
    {
        public Vector2 Position { get; set; }

        /// <summary>
        ///     In radians, + = clockwise
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        ///     1 = No Zoom
        ///     2 = 2x Zoom
        /// </summary>
        public double Zoom { get; set; }

        public View(Vector2 startPosition, double startZoom = 1.0, double startRotation = 0.0)
        {
            Position = startPosition;
            Zoom = startZoom;
            Rotation = startRotation;
        }

        public void ApplyTransform()
        {
            var transform = Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-Position.X, -Position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ((float)Rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)Zoom, (float)Zoom, 1.0f));

            GL.MultMatrix(ref transform);
        }

        public void Update() { }
    }
}