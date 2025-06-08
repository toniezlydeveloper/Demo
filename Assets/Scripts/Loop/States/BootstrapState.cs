using System;
using Internal.Runtime.Flow.States;
using UnityEngine.InputSystem;

namespace Loop.States
{
    public class BootstrapState : AState
    {
        private PlayerInput _input;

        public BootstrapState(PlayerInput input) => _input = input;

        public override void OnEnter() => DisableInput();

        public override Type OnUpdate() => typeof(MenuLoadingState);

        private void DisableInput() => _input.actions.Disable();
    }
}