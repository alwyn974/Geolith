using Godot;

namespace Geolith.StateMachine
{
    [GlobalClass]
    public partial class State : Node
    {
        [Export] public CharacterBody3D Entity;
        [Signal] public delegate void TransitionEventHandler(State currentState, string newStateName);

        public virtual void Enter()
        {
        }

        public virtual void Update(double delta)
        {
        }

        public virtual void PhysicsUpdate(double delta)
        {
        }

        public virtual void Exit()
        {
        }
    }
}
