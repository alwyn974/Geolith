using Godot;

namespace Geolith.Player
{
    public partial class Player : CharacterBody3D
    {
        [Export] public float Speed = 5.0f;
        [Export] public float JumpForce = 5.0f;

        private float _gravity =
            ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

        public override void _PhysicsProcess(double delta)
        {
            Vector3 velocity = Velocity;

            if (!IsOnFloor())
                velocity.Y -= _gravity * (float)delta;

            if (Input.IsActionJustPressed("jump") && IsOnFloor())
                velocity.Y = JumpForce;

            Velocity = velocity;
            MoveAndSlide();
        }
    }
}
