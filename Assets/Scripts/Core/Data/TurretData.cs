using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(menuName = "IntowerviewDefense/TurretData")]
    public class TurretData : ScriptableObject
    {
        public GameObject[] prefab;

        [Range(1, 10)]
        public int damage = 1;

        [Range(1, 5)]
        public float range = 10;

        [Range(.1f, 5)]
        public float shootEvery = 1;
        
    }
}