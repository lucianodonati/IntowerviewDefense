using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(menuName = "IntowerviewDefense/TileData")]
    public class TileData : ScriptableObject
    {
        public GameObject plain;
        [Range(0, 1)] public float plainChance = .5f;
        public GameObject[] unplayable;
        public GameObject path;
        public GameObject[] start;
        public GameObject[] end;
    }
}