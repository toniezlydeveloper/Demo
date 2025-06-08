using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class ShotRedirector : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D redirectorRigidbody;
        
        private List<Bullet> _redirectedBullets = new();

        private void OnEnable() => ResetRedirection();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!TryGetBullet(other, out Bullet bullet))
            {
                return;
            }

            Redirect(bullet);
        }

        private void ResetRedirection() => _redirectedBullets.Clear();

        private bool TryGetBullet(Collider2D other, out Bullet bullet)
        {
            if (!other.TryGetComponent(out bullet))
            {
                return false;
            }

            return !_redirectedBullets.Contains(bullet);
        }

        private void Redirect(Bullet bullet)
        {
            bullet.Redirect(redirectorRigidbody);
            _redirectedBullets.Add(bullet);
        }
    }
}