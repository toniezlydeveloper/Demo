using System.Linq;
using UnityEngine;

namespace Winning
{
    public class DestructionCondition : AWinCondition
    {
        [SerializeField] private GameObject[] objectsToDestroy;

        public override bool IsFulfilled => objectsToDestroy.All(objectToDestroy => objectToDestroy == null);
    }
}