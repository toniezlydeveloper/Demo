using System.Collections;
using Internal.Runtime.Utilities;
using NaughtyAttributes;
using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(Damageable))]
    public class DeathDestroyer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer damageableRenderer;
        [SerializeField] private float postHitEffectLifetime;
        [SerializeField] private GameObject deathEffect;
        [Layer]
        [SerializeField] private int deadLayer;
        [SerializeField] private float delay;
        
        private void Start() => GetComponent<Damageable>().OnDied += DestroySelf;

        private void OnDestroy() => GetComponent<Damageable>().OnDied -= DestroySelf;

        private void DestroySelf() => StartCoroutine(DestroySelfRoutine());

        private IEnumerator DestroySelfRoutine()
        {
            gameObject.OverrideLayer(deadLayer);
            
            yield return new WaitForSeconds(delay);
            
            deathEffect.SetActive(true);
            damageableRenderer.Disable();

            yield return new WaitForSeconds(postHitEffectLifetime);
            
            gameObject.DestroySelf();
        }
    }
}