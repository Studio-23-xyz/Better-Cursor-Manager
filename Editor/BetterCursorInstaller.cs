using UnityEditor;
using UnityEngine;


namespace Studio23.SS2.BetterCursor.Editor
{
    public class BetterCursorInstaller : UnityEditor.Editor
    {
        [MenuItem("Studio-23/BetterCursor/Install Cursor", false, 10)]
        static void Install()
        {
            GameObject prefab = Resources.Load<GameObject>("BetterCursor/CursorCanvas");

            if (prefab != null)
            {
                GameObject instantiatedObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instantiatedObject!.name = "Better Cursor Canvas";
            }
            else
            {
                Debug.LogError("Failed to Setup Better Cursor! Try to set it up manually");
            }
        }
    }
}