using Core.Grid.Tiles;
using UnityEditor;

namespace Core.Editor
{
    [CustomEditor(typeof(Tile)), CanEditMultipleObjects]
    public class TileEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}