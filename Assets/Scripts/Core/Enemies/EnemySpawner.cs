using System;
using System.Collections;
using Core.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Enemy[] enemyPrefabs = null;

        [SerializeField]
        private Transform[] waypoints = null;

        [SerializeField]
        private SpawnedEnemies spawnedEnemiesSO = null;

        private Vector2 spawnRate = new Vector2(1, 5);


        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            do
            {
                Spawn();
                yield return new WaitForSeconds(Random.Range(spawnRate.x, spawnRate.y));
            } while (true);
        }

        private void Spawn()
        {
            var newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform);
            newEnemy.transform.position = waypoints[0].position;
            newEnemy.waypoints = waypoints;

            spawnedEnemiesSO.spawnedEnemies.Add(newEnemy);
        }

        public void DestroyEnemy(Enemy toBeKilled)
        {
            spawnedEnemiesSO.spawnedEnemies.Remove(toBeKilled);
            Destroy(toBeKilled.gameObject);
        }

        private void OnDestroy()
        {
            spawnedEnemiesSO.spawnedEnemies.Clear();
        }
    }
}