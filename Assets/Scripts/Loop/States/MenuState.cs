using Internal.Runtime.Dependencies.Core;
using Internal.Runtime.Flow.States;
using Internal.Runtime.Utilities;

namespace Loop.States
{
    public class MenuState : AState
    {
        private DependencyRecipe<DependencyList<MonoBehaviourToggle>> _toggles = DependencyInjector.GetRecipe<DependencyList<MonoBehaviourToggle>>();

        public override void OnEnter() => ToggleNonMenuBehaviours(false);

        public override void OnExit() => ToggleNonMenuBehaviours(true);

        private void ToggleNonMenuBehaviours(bool state)
        {
            foreach (MonoBehaviourToggle toggle in _toggles.Value)
            {
                toggle.Toggle(state);
            }
        }
    }
}