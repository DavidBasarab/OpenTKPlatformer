using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKPlatformer
{
    public enum TweenType
    {
        Instant,
        Linear,
        QuadraticInOut,
        CubicInOut,
        QuarticOut
    }

    public class View
    {
        private int _currentStep;
        private Vector2 _positionFrom;

        private int _tweenSteps;

        private TweenType _tweenType;

        public Vector2 Position { get; private set; }

        public Vector2 PositionGoTo { get; private set; }

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

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
            _positionFrom = newPosition;
            PositionGoTo = newPosition;
            _tweenType = TweenType.Instant;
            _currentStep = 0;
            _tweenSteps = 0;
        }

        public void SetPosition(Vector2 newPosition, TweenType type, int numberOfSteps)
        {
            _positionFrom = Position;
            Position = newPosition;
            PositionGoTo = newPosition;
            _tweenType = type;
            _currentStep = 0;
            _tweenSteps = numberOfSteps;
        }

        public Vector2 ToWorld(Vector2 input)
        {
            input /= (float)Zoom;

            var dx = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            var dy = new Vector2((float)Math.Cos(Rotation + MathHelper.PiOver2), (float)Math.Sin(Rotation + MathHelper.PiOver2));

            return Position + dx * input.X + dy * input.Y;
        }

        public void Update()
        {
            if (_currentStep < _tweenSteps)
            {
                _currentStep++;

                switch (_tweenType)
                {
                    case TweenType.Linear:
                        Position = _positionFrom + (PositionGoTo - _positionFrom) * GetLinear((float)_currentStep / _tweenSteps);
                        break;
                    case TweenType.QuadraticInOut:
                        Position = _positionFrom + (PositionGoTo - _positionFrom) * GetQuadraticInOut((float)_currentStep / _tweenSteps);
                        break;
                    case TweenType.CubicInOut:
                        Position = _positionFrom + (PositionGoTo - _positionFrom) * GetCubicInOut((float)_currentStep / _tweenSteps);
                        break;
                    case TweenType.QuarticOut:
                        Position = _positionFrom + (PositionGoTo - _positionFrom) * GetQuarticOut((float)_currentStep / _tweenSteps);
                        break;
                }
            }
            else Position = PositionGoTo;
        }

        private float GetCubicInOut(float ratio)
        {
            return ratio * ratio * ratio / (3 * ratio * ratio - 3 * ratio + 1);
        }

        private float GetLinear(float ratio)
        {
            return ratio;
        }

        private float GetQuadraticInOut(float ratio)
        {
            return ratio * ratio / (2 * ratio * ratio - 2 * ratio + 1);
        }

        private float GetQuarticOut(float ratio)
        {
            return -((ratio - 1) * (ratio - 1) * (ratio - 1) * (ratio - 1)) + 1;
        }
    }
}