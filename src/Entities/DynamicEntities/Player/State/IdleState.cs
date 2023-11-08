using Godot;

//TODO create enums for the player states

namespace Geolith.Player.State
{
    [GlobalClass]
    public partial class IdleState : StateMachine.State
    {
        public override void Enter()
        {
            GD.Print("IdleState.Enter");
        }

        public override void Update(double delta)
        {
        }

        public override void PhysicsUpdate(double delta)
        {
            if (Input.GetVector("move_l", "move_r", "move_fw", "move_bw") != Vector2.Zero)
                EmitSignal(nameof(Transition), this, "MoveState");
        }

        public override void Exit()
        {
            GD.Print("IdleState.Exit");
        }
    }
}
