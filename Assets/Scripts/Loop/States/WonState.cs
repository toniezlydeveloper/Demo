using Internal.Runtime.Flow.States;
using Internal.Runtime.Utilities;
using UnityEngine.InputSystem;

namespace Loop.States
{
    public class WonState : AState
    {
        private PlayerInput _input;

        private const float TimeScaleAfterGame = 0.5f;

        public WonState(PlayerInput input) => _input = input;

        public override void OnEnter()
        {
            TimeHelper.SetTimeScale(TimeScaleAfterGame);
            DisableInput();
        }

        public override void OnExit() => TimeHelper.SetDefaultTimeScale();

        private void DisableInput() => _input.actions.Disable();
    }
}