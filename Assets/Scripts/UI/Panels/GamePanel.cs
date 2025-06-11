using System;
using Internal.Runtime.Flow.UI;
using Loop.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class GamePanel : AUIPanel, IPausePanel
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button exitButton;

        private Action _requireToggleCallback;
        
        private void Start() => AddButtonCallbacks();
        
        public void Present(PauseInfo info)
        {
            Cache(info);
            ShowToggle(info);
        }

        private void AddButtonCallbacks()
        {
            menuButton.onClick.AddListener(RequestTransition<MenuLoadingState>);
            exitButton.onClick.AddListener(RequestTransition<ExitState>);
            resumeButton.onClick.AddListener(RequestPauseToggle);
        }

        private void Cache(PauseInfo info) => _requireToggleCallback = info.TogglePauseCallback;

        private void ShowToggle(PauseInfo info) => pausePanel.SetActive(info.IsPaused);

        private void RequestPauseToggle() => _requireToggleCallback?.Invoke();
    }
}