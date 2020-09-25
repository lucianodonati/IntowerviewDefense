using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private static readonly float reachedGoalSqrThreshold = .1f;
        private static readonly Vector2 enemySpeed = new Vector2(.5f, 2);

        private static EnemySpawner spawner;

        [SerializeField]
        private GameObject boomPrefab = null;

        public Transform[] waypoints = null;
        private int currentGoal = 0;

        private int health = 10;

        public int Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                {
                    spawner.DestroyEnemy(this);
                }
            }
        }

        private void Awake()
        {
            if (null == spawner)
                spawner = FindObjectOfType<EnemySpawner>();
        }

        private void Start()
        {
            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            while (currentGoal < waypoints.Length)
            {
                yield return null;
                var toGoalVector = transform.position - waypoints[currentGoal].position;
                var toGoalDirection = toGoalVector.normalized;

                transform.Translate(toGoalDirection * (Random.Range(enemySpeed.x, enemySpeed.y) * Time.deltaTime));

                if (toGoalVector.sqrMagnitude < reachedGoalSqrThreshold)
                {
                    currentGoal++;
                }
            }

            Debug.LogError("YOU LOSE, THE INTERVIWER WINS! MUAHAHAHAH");
            Debug.Break();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Projectile"))
            {
                Instantiate(boomPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}