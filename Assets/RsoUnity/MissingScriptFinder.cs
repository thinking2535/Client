using UnityEngine;
using UnityEditor;
using System;

#if UNITY_EDITOR
public class MissingScriptFinder : EditorWindow
{
    [MenuItem("Window/FindMissingScripts")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MissingScriptFinder));
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in selected prefabs"))
            FindInSelected();
    }
    private static void FindInSelected()
    {
        Int32 ScannedObjectCount = 0;
        Int32 ScannedComponentCount = 0;
        Int32 MissingCount = 0;

        foreach (var o in Selection.gameObjects)
        {
            ++ScannedObjectCount;

            var Components = o.GetComponents<Component>();
            for (Int32 i = 0; i < Components.Length; i++)
            {
                ++ScannedComponentCount;

                if (Components[i] == null)
                {
                    ++MissingCount;

                    var MissingObjectName = o.name;
                    var ObjectTransform = o.transform;

                    while (ObjectTransform.parent != null)
                    {
                        MissingObjectName = ObjectTransform.parent.name + "/" + MissingObjectName;
                        ObjectTransform = ObjectTransform.parent;
                    }

                    Debug.Log(MissingObjectName + " has an empty script attached in position : " + i);
                }
            }
        }

        Debug.Log(string.Format("Searched [{0}]GameObjects [{1}]components found [{2}]missing", ScannedObjectCount, ScannedComponentCount, MissingCount));
    }
}
#endif
