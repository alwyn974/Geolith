using Godot;

namespace Geolith.Camera
{
    public partial class CameraController : Node3D
    {
        [Export] public float MouseSensitivity = 0.5f;

        private Node3D _player;

        public override void _Ready()
        {
            _player = GetTree().GetNodesInGroup("Player")[0] as Node3D;

            TopLevel = true;
            Input.MouseMode = Input.MouseModeEnum.Captured;
        }

        public override void _Process(double delta)
        {
            Position = _player.Position;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is not InputEventMouseMotion mouseMotion) return;

            float x = RotationDegrees.X - mouseMotion.Relative.Y * MouseSensitivity;
            x = Mathf.Clamp(x, -90, 90);

            float y = RotationDegrees.Y - mouseMotion.Relative.X * MouseSensitivity;
            y = Mathf.Wrap(y, -180, 180);

            RotationDegrees = new Vector3(x, y, 0);
        }
    }
}
