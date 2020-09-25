#if UNITY_EDITOR
using System.Collections.Generic;
using Core.Grid.Tiles;
using JetBrains.Annotations;
using Tools;
using Unity.Collections;
using UnityEngine;

namespace Core
{
    [ExecuteInEditMode]
    public class GridCreator : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int size = new Vector2Int(10, 10);

        [SerializeField]
        private Tile tilePrefab = null;

        [SerializeField]
        private List<Tile> grid = null;

        //[EditorButton, UsedImplicitly]
        private void CreateGrid()
        {
            Clean();
            Create();
        }

        [EditorButton]
        private void Clean()
        {
            grid.Clear();

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private void Create()
        {
            var tileCount = size.x * size.y;

            grid = new List<Tile>(tileCount);

            name = $"Board ({tileCount})";

            int halfWidth = size.x / 2, halfDepth = size.y / 2;
            int tileCounter = 0;

            for (int x = -halfWidth; x < halfWidth; x++)
            {
                for (int z = -halfDepth; z < halfDepth; z++)
                {
                    var newTile = Instantiate(tilePrefab, transform);
                    newTile.gameObject.transform.localPosition = new Vector3(x, transform.localPosition.y, z);
                    newTile.name = $"Tile {(++tileCounter)} ({x},{z})";
                    newTile.IsEdgeTile = IsEdgeTile(x, halfWidth, z, halfDepth);
                    newTile.SpawnTile();

                    grid.Add(newTile);
                }
            }
        }

        private bool IsEdgeTile(int x, int halfWidth, int z, int halfDepth)
        {
            return x == -halfWidth || z == -halfDepth || x == halfWidth - 1 || z == halfDepth - 1;
        }

        [EditorButton, UsedImplicitly]
        private void UpdateTiles()
        {
            foreach (var tile in grid)
                tile.SpawnTile();
        }
    }
}
#endif