using Godot;

namespace Geolith.StateMachine
{
    [GlobalClass]
    public partial class State : Node
    {
        [Signal] public delegate void TransitionEventHandler(State currentState, string newStateName);

        /**
         * Enter is called when the state is entered.
         * It is used to initialize the state.
         */
        public virtual void Enter()
        {
        }

        /**
         * Update is called every frame.
         */
        public virtual void Update(double delta)
        {
        }

        /**
         * PhysicsUpdate is called every physics frame.
         */
        public virtual void PhysicsUpdate(double delta)
        {
        }

        /**
         * Exit is called when the state is exited.
         * It is used to clean up the state.
         */
        public virtual void Exit()
        {
        }
    }
}
