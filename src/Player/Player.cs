using Godot;

namespace Geolith.Player
{
    public partial class Player : CharacterBody3D
    {
        [Export] public float Speed = 5.0f;
        [Export] public float JumpForce = 5.0f;

        private AnimationTree _animationTree;

        private float _gravity =
            ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

        public override void _Ready()
        {
            _animationTree = GetNode<AnimationTree>("AnimationTree");
            _animationTree.Active = true;
        }

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


        public override void _UnhandledKeyInput(InputEvent @event)
        {
            if (@event is InputEventKey keyEvent)
            {
                //Check for the "Emote 1" action. The action is defined in the project settings.
                if (keyEvent.IsActionPressed("emote_1"))
                    _animationTree.Set("Emote", true);
                else if (keyEvent.IsActionReleased("emote_1"))
                    _animationTree.Set("Emote", false);
            }
        }
    }
}
