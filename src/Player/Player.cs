using Godot;

namespace Geolith.Player
{
    public partial class Player : CharacterBody3D
    {
        [Export] public float Speed = 5.0f;
        [Export] public float JumpVelocity = 4.5f;

        private float _gravity =
            ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

        public SpringArm3D SpringArm;

        public override void _Ready()
        {
            SpringArm = GetNode<SpringArm3D>("SpringArm");
        }

        public override void _PhysicsProcess(double delta)
        {
            Vector3 velocity = Velocity;

            if (!IsOnFloor())
                velocity.Y -= _gravity * (float)delta;

            if (Input.IsActionJustPressed("jump") && IsOnFloor())
                velocity.Y = JumpVelocity;

            Velocity = velocity;
            MoveAndSlide();
        }

        public override void _Process(double delta)
        {
            SpringArm.Position = Position;
        }
    }
}
