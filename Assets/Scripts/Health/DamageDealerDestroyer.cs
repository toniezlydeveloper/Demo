using Internal.Runtime.Utilities;
using UnityEngine;

namespace Health
{
    public class DamageDealerDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            ExtendedDebug.Log("Getting into trigger");
            
            if (!TryGetDealer(other, out CollisionDamageDealer dealer))
            {
                return;
            }

            ExtendedDebug.Log("Destroying dealer");
            Destroy(dealer);
        }

        private bool TryGetDealer(Collider2D other, out CollisionDamageDealer dealer) => other.TryGetComponent(out dealer);

        private void Destroy(CollisionDamageDealer dealer) => dealer.gameObject.DestroySelf();
    }
}