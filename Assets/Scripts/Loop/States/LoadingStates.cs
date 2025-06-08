using System;
using Internal.Runtime.Flow.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Loop.States
{
    public class MenuLoadingState : ALoadingState<MenuState>
    {
        public MenuLoadingState(string levelName, float minLoadingDuration) : base(levelName, minLoadingDuration)
        {
        }
    }
    
    public class GameLoadingState : ALoadingState<GameState>
    {
        public GameLoadingState(string levelName, float minLoadingDuration) : base(levelName, minLoadingDuration)
        {
        }
    }
    
    public abstract class ALoadingState<TNextSate> : AState where TNextSate : AState
    {
        private float _minLoadingDuration;
        private string _levelName;
        
        private AsyncOperation _loadingOperation;
        private float _minLoadingTime;

        protected ALoadingState(string levelName, float minLoadingDuration)
        {
            _minLoadingDuration = minLoadingDuration;
            _levelName = levelName;
        }

        public override void OnEnter()
        {
            CacheLoadingOperation(SceneManager.LoadSceneAsync(GetLevelName()));
            CacheLoadingTime();
        }

        public override Type OnUpdate() => HasLoadedLevel() ? typeof(TNextSate) : null;

        private string GetLevelName() => _levelName;

        private void CacheLoadingOperation(AsyncOperation loadingOperation) => _loadingOperation = loadingOperation;

        private void CacheLoadingTime() => _minLoadingTime = Time.unscaledTime + _minLoadingDuration;

        private bool HasLoadedLevel()
        {
            if (!_loadingOperation.isDone)
            {
                return false;
            }
            
            return Time.unscaledTime >= _minLoadingTime;
        }
    }
}