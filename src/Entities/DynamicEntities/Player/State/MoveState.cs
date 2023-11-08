using System;
using Godot;

namespace Geolith.Entities.DynamicEntities.Player.State
{
    [GlobalClass]
    public partial class MoveState : StateMachine.State
    {
        private Player _player;

        public override void Enter()
        {
            _player = Entity as Player;
        }

        public override void Update(double delta)
        {
        }

        public override void PhysicsUpdate(double delta)
        {
            Vector2 inputDir = Input.GetVector("move_l", "move_r", "move_fw", "move_bw");
            Vector3 velocity = (_player.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y));

            velocity = velocity.Rotated(Vector3.Up, _player.SpringArm.Rotation.Y).Normalized();

            if (velocity != Vector3.Zero)
            {
                velocity.X = velocity.X * _player.Speed;
                velocity.Z = velocity.Z * _player.Speed;

                var lookDir = velocity;
                lookDir.Y *= _player.Speed;
                _player.LookAt(_player.GlobalTransform.Origin + lookDir, Vector3.Up);
            }
            else
            {
                velocity.X = Mathf.MoveToward(_player.Velocity.X, 0, _player.Speed);
                velocity.Z = Mathf.MoveToward(_player.Velocity.Z, 0, _player.Speed);

                EmitSignal(nameof(Transition), this, "IdleState");
            }

            velocity.Y = _player.Velocity.Y;
            _player.Velocity = velocity;
        }

        public override void Exit()
        {
        }
    }
}
