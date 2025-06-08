using Internal.Runtime.Utilities;
using UnityEngine;

namespace Health
{
    public class CollisionDamageDealer : MonoBehaviour
    {
        [SerializeField] private int damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            ExtendedDebug.Log("Getting in the trigger");
            
            if (!TryGetDamageable(other, out Damageable damageable))
            {
                return;
            }

            ExtendedDebug.Log("Dealing damage");
            DealDamage(damageable);
            DestroySelf();
        }

        private bool TryGetDamageable(Collider2D other, out Damageable damageable) => other.TryGetComponent(out damageable);

        private void DealDamage(Damageable damageable) => damageable.TakeDamage(damage);

        private void DestroySelf() => gameObject.DestroySelf();
    }
}