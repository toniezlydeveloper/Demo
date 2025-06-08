using System.Collections.Generic;
using UnityEngine;

namespace Parallax
{
    // There should be 3 backgrounds: left, center, right
    // Left background should have index 0
    // Center background should have index 1
    // Right background should have index 2
    public class Layer : MonoBehaviour
    {
        [SerializeField] private List<Transform> backgrounds;
        [SerializeField] private float scrollSpeed;
        [SerializeField] private float width;

        private Vector3 _horizontalRepositionFactor;
        private float _halvedWidth;

        private const int BackgroundsCount = 3;

        private void Start() => InitValues();

        public void Move(float xDelta, float cameraX)
        {
            MoveBackgrounds(xDelta);
            
            if (CrossedToTheRight(cameraX))
            {
                MoveLeftBackgroundToTheRight();
            }
            
            if (CrossedToTheLeft(cameraX))
            {
                MoveRightBackgroundToTheLeft();
            }
        }

        private void MoveBackgrounds(float xDelta)
        {
            foreach (Transform background in backgrounds)
            {
                background.position += Time.deltaTime * xDelta * scrollSpeed * Vector3.right;
            }
        }

        private bool CrossedToTheRight(float cameraX) => cameraX > backgrounds[1].position.x + _halvedWidth;

        private bool CrossedToTheLeft(float cameraX) => cameraX < backgrounds[1].position.x - _halvedWidth;

        private void MoveLeftBackgroundToTheRight()
        {
            Transform leftBackground = backgrounds[0];
            leftBackground.position += _horizontalRepositionFactor;
            
            backgrounds.Remove(leftBackground);
            backgrounds.Add(leftBackground);
        }

        private void MoveRightBackgroundToTheLeft()
        {
            Transform rightBackground = backgrounds[2];
            rightBackground.position -= _horizontalRepositionFactor;
            
            backgrounds.Remove(rightBackground);
            backgrounds.Insert(0, rightBackground);
        }

        private void InitValues()
        {
            _horizontalRepositionFactor =  width * BackgroundsCount * Vector3.right;
            _halvedWidth = width * 0.5f;
        }
    }
}