using UnityEngine;

namespace Parallax
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Layer[] layers;

        private Transform _cameraTransform;
        private Vector3 _lastPosition;

        private void Start()
        {
            _cameraTransform = Camera.main!.transform;
            _lastPosition = _cameraTransform.position;
        }

        private void Update()
        {
            float cameraX = _cameraTransform.position.x;
            float xDelta = cameraX - _lastPosition.x;
            
            foreach (Layer layer in layers)
            {
                layer.Move(xDelta, cameraX);
            }

            _lastPosition = _cameraTransform.position;
        }
    }
}