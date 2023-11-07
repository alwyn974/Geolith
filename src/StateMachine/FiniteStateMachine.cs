using Godot;
using Godot.Collections;

namespace Geolith.StateMachine
{
    [GlobalClass]
    public partial class FiniteStateMachine : Node
    {
        [Export] public State InitialState = null;

        private State _currentState;
        private Dictionary<string, State> _states;

        public override void _Ready()
        {
            _states = new Dictionary<string, State>();

            foreach (Node child in GetChildren())
            {
                if (child is State state)
                {
                    _states.Add(state.Name, state);
                    state.Transition += _ChangeState;
                }
            }

            if (InitialState is not null)
            {
                _currentState = InitialState;
                _currentState.Enter();
            }
        }

        public override void _Process(double delta)
        {
            _currentState?.Update(delta);
        }

        public override void _PhysicsProcess(double delta)
        {
            _currentState?.PhysicsUpdate(delta);
        }

        private void _ChangeState(State currentState, string newStateName)
        {
            if (currentState != _currentState) return;

            State newState = _states[newStateName];

            if (newState is null || newState == _currentState)
            {
                GD.PrintErr($"State {newStateName} does not exist or is the same as the current state.");
                return;
            }

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}
