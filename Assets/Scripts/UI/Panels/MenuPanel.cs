using Internal.Runtime.Flow.UI;
using Loop.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels
{
    public class MenuPanel : AUIPanel
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;

        private void Start()
        {
            playButton.onClick.AddListener(RequestTransition<GameState>);
            exitButton.onClick.AddListener(RequestTransition<ExitState>);
        }
    }
}