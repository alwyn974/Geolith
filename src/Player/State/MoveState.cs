using System;
using Godot;

namespace Geolith.Player.State
{
    [GlobalClass]
    public partial class MoveState : StateMachine.State
    {
        private Player _player;
        //TODO Remove this when the camera is correctly implemented
        private Node3D _lookAtNode;

        public override void Enter()
        {
            //TODO find a better way to do this
            _player = GetTree().GetNodesInGroup("Player")[0] as Player;
            _lookAtNode = GetTree().GetNodesInGroup("CameraController")[0] as Node3D;
        }

        public override void PhysicsUpdate(double delta)
        {
            Vector2 inputDir = Input.GetVector("move_l", "move_r", "move_fw", "move_bw");
            Vector3 velocity = (_player.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y));

            if (velocity != Vector3.Zero)
            {
                //TODO find a mathematically correct way to do this
                var lookAt = new Vector3(_lookAtNode.GlobalPosition.X, _player.GlobalPosition.Y,
                    _lookAtNode.GlobalPosition.Z);
                _player.LookAt(lookAt, Vector3.Up);
                velocity *= _player.Speed;
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
    }
}
