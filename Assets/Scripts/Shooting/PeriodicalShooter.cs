using UnityEngine;

namespace Shooting
{
    public class PeriodicalShooter : MonoBehaviour
    {
        [SerializeField] private Animator shooterAnimator;
        [SerializeField] private Transform shotPoint;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float interval;
        
        private static readonly int ShootingHash = Animator.StringToHash("Shooting");

        private void Start() => InvokeRepeating(nameof(TryShooting), interval, interval);

        private void TryShooting()
        {
            if (!ShouldShoot())
            {
                return;
            }

            AnimateShot();
        }

        // It is called by the Animator event
        private void Shot() => Shot(Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity));

        private void AnimateShot() => shooterAnimator.SetTrigger(ShootingHash);

        private bool ShouldShoot() => isActiveAndEnabled;
        
        private void Shot(Bullet bullet) => bullet.Shot(shotPoint.right);
    }
}