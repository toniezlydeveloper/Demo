using Internal.Runtime.Dependencies.Core;
using UnityEngine;

namespace Health
{
    public class Player : Damageable, IDependency
    {
        [SerializeField] private GameObject[] healthIndicators;
        [SerializeField] private MonoBehaviour[] controllers;
        [SerializeField] private GameObject[] aliveObjects;
        
        public Quaternion Rotation => transform.rotation;
        public Vector3 Position => transform.position;
        public Transform Center => transform;

        private void OnEnable()
        {
            OnGotHurt += RefreshHealthPresentation;
            OnDied += HandleDeath;
        }

        private void OnDisable()
        {
            OnGotHurt -= RefreshHealthPresentation;
            OnDied -= HandleDeath;
        }

        private void HandleDeath()
        {
            DisableInteraction();
            DisableAliveObjects();
        }

        private void DisableInteraction()
        {
            foreach (MonoBehaviour controller in controllers)
            {
                controller.enabled = false;
            }
        }

        private void DisableAliveObjects()
        {
            foreach (GameObject aliveObject in aliveObjects)
            {
                aliveObject.SetActive(false);
            }
        }

        private void RefreshHealthPresentation(int postDamageHealth)
        {
            for (int i = postDamageHealth; i < healthIndicators.Length; i++)
            {
                healthIndicators[i].SetActive(false);
            }
            
            for (int i = 0; i < postDamageHealth; i++)
            {
                healthIndicators[i].SetActive(true);
            }
        }
    }
}