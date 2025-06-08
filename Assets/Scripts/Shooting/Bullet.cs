using UnityEngine;

namespace Shooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem postHitEffectPrefab;
        [SerializeField] private Rigidbody2D bulletRigidbody;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private float postHitEffectLifetime;
        [SerializeField] private float redirectionPower;
        [SerializeField] private float shotPower;
        [SerializeField] private float lifetime;
        
        private void Start() => Destroy(gameObject, lifetime);

        private void OnDestroy() => Destroy(Instantiate(postHitEffectPrefab, transform.position, Quaternion.identity), postHitEffectLifetime);

        public void Shot(Vector3 direction) => bulletRigidbody.velocity = direction * shotPower;

        public void Redirect(Rigidbody2D redirector)
        {
            bulletRigidbody.velocity = -redirectionPower * bulletRigidbody.velocity + redirector.velocity;
            trailRenderer.Clear();
        }
    }
}