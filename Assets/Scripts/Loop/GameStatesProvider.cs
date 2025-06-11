using Health;
using Internal.Runtime.Dependencies.Core;
using Internal.Runtime.Flow.States;
using Internal.Runtime.Utilities;
using Loop.States;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Winning;

namespace Loop
{
    public class GameStatesProvider : AStatesProvider
    {
        [Header("General")]
        [SerializeField] private GameObject uiParent;
        [SerializeField] private PlayerInput input;
        
        [Header("Level")]
        [SerializeField] private InputActionReference pauseInput;
        [SerializeField] private VolumeProfile volumeProfile;
        [SerializeField] private Player playerPrefab;
        
        [Header("Loading")]
        [SerializeField] private float minLoadingDuration;
        [Scene]
        [SerializeField] private string levelName;

        private IPausePanel _pausePanel;
        
        private void Start()
        {
            GetReferences();
            InjectListRecipes();
            AddStates();
        }

        private void InjectListRecipes()
        {
            DependencyInjector.InjectListRecipe<MonoBehaviourToggle>();
            DependencyInjector.InjectListRecipe<AWinCondition>();
        }

        private void AddStates()
        {
            AddInitialState(new BootstrapState(input));
            AddState(new MenuLoadingState(levelName, minLoadingDuration));
            AddState(new MenuState());
            AddState(new GameState(_pausePanel, pauseInput, input, playerPrefab, volumeProfile));
            AddState(new WonState(input));
            AddState(new GameLoadingState(levelName, minLoadingDuration));
            AddState(new ExitState());
        }

        private void GetReferences() => _pausePanel = GetFromUI<IPausePanel>();

        private TDependency GetFromUI<TDependency>() where TDependency : IDependency => uiParent.GetComponentInChildren<TDependency>();
    }
}