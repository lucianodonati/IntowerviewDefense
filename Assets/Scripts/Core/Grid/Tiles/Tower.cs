using Core.Damage;
using Core.Data;
using Tools;
using UnityEngine;

namespace Core.Grid.Tiles
{
    public class Tower : BaseTile
    {
        [SerializeField]
        private TurretData turretData = null;

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
                Instantiate(GetRandomFromArray(turretData.prefab), turretParent);
            }
        }
#endif

        #endregion
    }
}