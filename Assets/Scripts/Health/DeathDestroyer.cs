using Internal.Runtime.Utilities;
using NaughtyAttributes;
using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(Damageable))]
    public class DeathDestroyer : MonoBehaviour
    {
        [Layer]
        [SerializeField] private int deadLayer;
        [SerializeField] private float delay;
        
        private void Start() => GetComponent<Damageable>().OnDied += DestroySelf;

        private void OnDestroy() => GetComponent<Damageable>().OnDied -= DestroySelf;

        private void DestroySelf()
        {
            gameObject.OverrideLayer(deadLayer);
            gameObject.DestroySelf(delay);
        }
    }
}