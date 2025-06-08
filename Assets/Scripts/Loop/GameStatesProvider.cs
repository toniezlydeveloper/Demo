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
        [Header("Level")]
        [SerializeField] private VolumeProfile volumeProfile;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private PlayerInput input;
        
        [Header("Loading")]
        [SerializeField] private float minLoadingDuration;
        [Scene]
        [SerializeField] private string levelName;
        
        private void Start()
        {
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
            AddState(new GameState(input, playerPrefab, volumeProfile));
            AddState(new WonState(input));
            AddState(new GameLoadingState(levelName, minLoadingDuration));
            AddState(new ExitState());
        }
    }
}