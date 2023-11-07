using Godot;

namespace Geolith.Camera
{
    public partial class SpringArm : SpringArm3D
    {
        private float _mouseSensitivity = 0.5f;

        public override void _Ready()
        {
            TopLevel = true;
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is not InputEventMouseMotion mouseMotion) return;

            float x = RotationDegrees.X - mouseMotion.Relative.Y * _mouseSensitivity;
            x = Mathf.Clamp(x, -90, 90);

            float y = RotationDegrees.Y - mouseMotion.Relative.X * _mouseSensitivity;
            y = Mathf.Wrap(y, -180, 180);

            RotationDegrees = new Vector3(x, y, 0);
        }
    }
}
