using System.Collections.Generic;
using Core.Enemies;
using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "SpawnedEnemies", menuName = "IntowerviewDefense/SpawnedEnemies", order = 0)]
    public class SpawnedEnemies : ScriptableObject
    {
        public List<Enemy> spawnedEnemies = new List<Enemy>();
    }
}