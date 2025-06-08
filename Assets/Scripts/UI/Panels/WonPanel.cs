using Internal.Runtime.Flow.UI;
using Loop.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class WonPanel : AUIPanel
    {
        [SerializeField] private Button replayButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button exitButton;
        
        private void Start()
        {
            replayButton.onClick.AddListener(RequestTransition<GameLoadingState>);
            menuButton.onClick.AddListener(RequestTransition<MenuLoadingState>);
            exitButton.onClick.AddListener(RequestTransition<ExitState>);
        }
    }
}