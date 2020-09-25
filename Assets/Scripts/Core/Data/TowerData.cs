using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(menuName = "IntowerviewDefense/TowerData")]
    public class TowerData : ScriptableObject
    {
        public GameObject[] bottoms, middles, tops;
    }
}