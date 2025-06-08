using Internal.Runtime.Flow.States;
using UnityEngine;

namespace Loop.States
{
    public class ExitState : AState
    {
        public override void OnEnter()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExecuteMenuItem("Edit/Play");
#endif
            Application.Quit();
        }
    }
}