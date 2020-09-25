using System;
using System.Text;
using Core.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Grid.Tiles
{
    [ExecuteAlways]
    public class Tile : BaseTile
    {
#if UNITY_EDITOR
        public enum Type
        {
            Unplayable,
            Path,
            Start,
            End,
            Custom
        }

        [SerializeField]
        private Type type = Type.Unplayable;
        
        [SerializeField]
        private Transform modelParent = null;

        [SerializeField]
        private TileData data = null;

        public override void SpawnTile()
        {
            if (type != Type.Custom)
                DestroyChildren(modelParent);

            GameObject prefab = null;
            string insert = $"[X] - ";
            StringBuilder insertBuilder = new StringBuilder(insert);

            switch (type)
            {
                case Type.Unplayable:
                    insertBuilder[1] = 'U';
                    if (!IsEdgeTile && Random.value <= data.plainChance)
                        prefab = data.plain;
                    else
                        prefab = GetRandomFromArray(data.unplayable);
                    break;
                case Type.Path:
                    insertBuilder[1] = 'P';
                    prefab = data.path;
                    break;
                case Type.Start:
                    insertBuilder[1] = 'S';
                    prefab = GetRandomFromArray(data.start);
                    break;
                case Type.End:
                    insertBuilder[1] = 'E';
                    prefab = GetRandomFromArray(data.end);
                    break;
                case Type.Custom:
                    insertBuilder[1] = 'C';
                    break;
            }

            if (name.StartsWith("["))
                name = insertBuilder + name.Substring(insert.Length, name.Length - insert.Length);
            else
                name = insertBuilder + name;

            if (null != prefab)
                Instantiate(prefab, modelParent);
        }



#endif
    }
}