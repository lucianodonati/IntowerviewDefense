using UnityEngine;

namespace Core.Grid
{
    public abstract class BaseTile : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool IsEdgeTile { get; set; }

        public abstract void SpawnTile();

        protected GameObject GetRandomFromArray(GameObject[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        protected void DestroyChildren(Transform parent) // Best name ever.
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.GetChild(i).gameObject);
            }
        }
#endif
    }
}