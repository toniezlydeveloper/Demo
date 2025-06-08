using Internal.Runtime.Flow.UI;
using Loop.States;
using UI.Panels;

namespace UI
{
    public class UIPanelsProvider : AUIPanelsProvider
    {
        protected override void AddTranslations()
        {
            AddTranslation<GameLoadingState, LoadingPanel>();
            AddTranslation<MenuLoadingState, LoadingPanel>();
            AddTranslation<BootstrapState, LoadingPanel>();
            AddTranslation<MenuState, MenuPanel>();
            AddTranslation<WonState, WonPanel>();
        }
    }
}