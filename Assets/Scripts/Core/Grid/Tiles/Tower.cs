using System;
using System.Collections;
using Core.Damage;
using Core.Data;
using Core.Enemies;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Grid.Tiles
{
    public class Tower : BaseTile
    {
        [SerializeField]
        private TurretData turretData = null;

        [SerializeField]
        private Projectile projectilePrefab = null;

        #region Tower Creation

#if UNITY_EDITOR
        [SerializeField]
        private Transform bottomParent = null, middleParent = null, topParent = null, turretParent = null;

        [SerializeField]
        private TowerData towerData = null;


        [EditorButton]
        public override void SpawnTile()
        {
            DestroyChildren(bottomParent);
            Instantiate(GetRandomFromArray(towerData.bottoms), bottomParent);

            DestroyChildren(middleParent);
            Instantiate(GetRandomFromArray(towerData.middles), middleParent);

            DestroyChildren(topParent);
            Instantiate(GetRandomFromArray(towerData.tops), topParent);

            DestroyChildren(turretParent);
            if (null != turretData)
            {
                var newTurret = Instantiate(GetRandomFromArray(turretData.prefab), turretParent).GetComponent<Turret>();
                newTurret.damage = turretData.damage;
            }
        }
#endif

        #endregion

        [SerializeField]
        private SpawnedEnemies spawnedEnemiesSO = null;

        private Transform currentTarget = null;
#if UNITY_EDITOR
        private Vector3 targetLocation;
#endif

        #region Target Acquisition

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

#if UNITY_EDITOR
                    targetLocation = enemyPos;
#endif

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

                newProjectile.transform.position = turretParent.position;

                newProjectile.Damage = turretData.damage;
                newProjectile.ShootInDirection(directionToEnemy,
                    directionToEnemy.magnitude * Random.Range(turretData.forceMinMax.x, turretData.forceMinMax.y));

            }
        }

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
    }
}