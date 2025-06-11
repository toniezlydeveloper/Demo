using UnityEngine;

namespace Shooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D bulletRigidbody;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private GameObject postHitEffect;
        [SerializeField] private float redirectionPower;
        [SerializeField] private float shotPower;
        [SerializeField] private float lifetime;
        
        private void OnEnable() => Destroy(gameObject, lifetime);

        private void OnDisable() => ShowHitEffect();

        public void Shot(Vector3 direction) => bulletRigidbody.velocity = direction * shotPower;

        public void Redirect(Rigidbody2D redirector)
        {
            bulletRigidbody.velocity = -redirectionPower * bulletRigidbody.velocity + redirector.velocity;
            trailRenderer.Clear();
        }

        private void ShowHitEffect()
        {
            postHitEffect.transform.SetParent(null);
            postHitEffect.SetActive(true);
        }
    }
}