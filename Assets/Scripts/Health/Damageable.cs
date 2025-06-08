using System;
using DG.Tweening;
using Internal.Runtime.Utilities;
using UnityEngine;

namespace Health
{
    public class Damageable : MonoBehaviour
    {
        public event Action<int> OnGotHurt;
        public event Action OnDied;
        
        [SerializeField] private SpriteRenderer hurtRenderer;
        [SerializeField] private Animator deathAnimator;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color hurtColor;
        [SerializeField] private int totalHealth;

        private Sequence _hurtAnimation;
        private int _currentHealth;
        
        private static readonly int DeathHash = Animator.StringToHash("Death");
        
        private const float HalvedHurtDuration = 0.15f;
        
        private void Start() => InitHealth();

        public void TakeDamage(int damage)
        {
            int postDamageHealth = GetPostDamageHealth(damage);
            ExtendedDebug.Log($"{name} taking damage");
            PresentHurting(DOTween.Sequence());
            OnGotHurt?.Invoke(postDamageHealth);
            
            if (!TryDying(postDamageHealth))
            {
                return;
            }

            ExtendedDebug.Log($"{name} dying");
            CancelHurtAnimation();
            OnDied?.Invoke();
            AnimateDeath();
        }

        private void PresentHurting(Sequence hurtAnimation)
        {
            CancelHurtAnimation();
            AnimateHurting(hurtAnimation);
            OverrideHurtAnimation(hurtAnimation);
        }

        private int GetPostDamageHealth(int damage) => _currentHealth - damage;

        private bool TryDying(int postDamageHealth)
        {
            _currentHealth = postDamageHealth;
            return _currentHealth <= 0;
        }

        private void InitHealth() => _currentHealth = totalHealth;

        private void AnimateHurting(Sequence hurtAnimation)
        {
            hurtAnimation.Append(hurtRenderer.DOColor(hurtColor, HalvedHurtDuration));
            hurtAnimation.Append(hurtRenderer.DOColor(defaultColor, HalvedHurtDuration));
            hurtAnimation.Play();
        }

        private void OverrideHurtAnimation(Sequence hurtAnimation) => _hurtAnimation = hurtAnimation;

        private void CancelHurtAnimation() => _hurtAnimation?.Kill();

        private void AnimateDeath() => deathAnimator.SetTrigger(DeathHash);
    }
}