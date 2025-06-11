using FMODUnity;
using UnityEngine;

namespace DefaultNamespace
{
    public class Audio : MonoBehaviour
    {

        private void Start()
        {
            RuntimeManager.PlayOneShot("event:/SFX/Explosion");
        }
    }
}