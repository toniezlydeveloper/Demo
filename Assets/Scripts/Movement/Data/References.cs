using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Movement.Data
{
    [Serializable]
    public class References
    {
        [field: SerializeField] public Rigidbody2D PlayerRigidbody { get; set; }
        [field: SerializeField] public Transform PlayerTransform { get; set; }
        [field: SerializeField] public Transform HealthTransform { get; set; }
        [field: SerializeField] public GameObject[] DashObjects { get; set; }
        [field: SerializeField] public VolumeProfile VolumeProfile { get; set; }
    }
}