#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    /// <summary>
    /// Stick this on a method
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class EditorButtonAttribute : PropertyAttribute
    {
    }

    [CustomEditor(typeof(MonoBehaviour), true)]
    public class EditorButton : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mono = target as MonoBehaviour;

            var methods = mono.GetType()
                .GetMembers(BindingFlags.Instance | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                            BindingFlags.NonPublic)
                .Where(o => Attribute.IsDefined(o, typeof(EditorButtonAttribute)));

            foreach (var memberInfo in methods)
            {
                if (GUILayout.Button(memberInfo.Name))
                {
                    var method = memberInfo as MethodInfo;
                    method.Invoke(mono, null);
                }
            }
        }
    }
#endif
}