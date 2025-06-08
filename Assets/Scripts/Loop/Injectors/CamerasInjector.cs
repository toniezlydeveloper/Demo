using Cinemachine;
using Internal.Runtime.Dependencies.Core;
using UnityEngine;

namespace States.Injectors
{
    public class CamerasInjector : ADependency<CamerasInjector>, IDependency
    {
        [field:SerializeField] public CinemachineVirtualCameraBase Menu { get; set; }
        [field:SerializeField] public CinemachineVirtualCameraBase Game { get; set; }
    }
}