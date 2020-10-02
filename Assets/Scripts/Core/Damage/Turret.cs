using System;
using System.Collections;
using Core.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Damage
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        private TurretData turretData = null;

        [SerializeField]
        private SpawnedEnemies spawnedEnemiesSO = null;

        [SerializeField]
        private Projectile projectilePrefab = null;

        public Vector2 forceMinMax = new Vector2(2, 5);


        private Transform currentTarget = null;


        #region Debug Gizmos

#if UNITY_EDITOR
        private Vector3 targetLocation;
#endif

        #endregion

        private void Start()
        {
            StartCoroutine(CheckForTargetInRangeRoutine());
        }

        private IEnumerator CheckForTargetInRangeRoutine()
        {
            do
            {
                yield return new WaitUntil(() => spawnedEnemiesSO.spawnedEnemies.Count > 0);

                for (int i = 0; null == currentTarget && i < spawnedEnemiesSO.spawnedEnemies.Count; i++)
                {
                    var enemyPos = spawnedEnemiesSO.spawnedEnemies[i].transform.position;
                    var sqrDistance = (enemyPos - transform.position).magnitude;

                    #region Debug Gizmos

#if UNITY_EDITOR
                    targetLocation = enemyPos;
#endif

                    #endregion

                    if (sqrDistance <= turretData.range)
                    {
                        currentTarget = spawnedEnemiesSO.spawnedEnemies[i].transform;
                    }
                }

                yield return StartCoroutine(AttackEnemyRoutine());
            } while (true);
        }

        IEnumerator AttackEnemyRoutine()
        {
            while (null != currentTarget)
            {
                yield return new WaitForSeconds(turretData.shootEvery);

                var directionToEnemy = (currentTarget.position - transform.position);

                var newProjectile = Instantiate(projectilePrefab, transform);

                newProjectile.transform.position = transform.position;

                newProjectile.Damage = turretData.damage;
                newProjectile.ShootInDirection(
                    directionToEnemy + Vector3.up * Random.Range(forceMinMax.x, forceMinMax.y),
                    directionToEnemy.magnitude);
            }
        }

        #region Debug Gizmos

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Vector3.zero != targetLocation)
            {
                var toTargetRange = transform.position +
                                    (targetLocation - transform.position).normalized * turretData.range;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, targetLocation);

                if (currentTarget == null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, toTargetRange);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(transform.position, currentTarget.position);
                }
            }
        }
#endif

        #endregion
    }
}