
using UnityEditor;
using UnityEngine;


namespace Studio23.SS2.BetterCursorManager.Editor
{
    public class BetterCursorUtility : UnityEditor.Editor
    {
        [MenuItem("Studio-23/BetterCursor/Install Cursor", false, 10)]
        static void InstantiatePrefab()
        {
            GameObject prefab = Resources.Load<GameObject>("CursorCanvas");

            if (prefab != null)
            {
                // Instantiate the prefab as a new GameObject in the scene.
                GameObject instantiatedObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                if (instantiatedObject != null)
                {
                    // Optionally, set the position, rotation, or other properties of the instantiated object.
                    // Example: instantiatedObject.transform.position = Vector3.zero;

                    // Rename the object if needed.
                    instantiatedObject.name = prefab.name;
                }
                else
                {
                    Debug.LogError("Failed to instantiate the prefab.");
                }
            }
            else
            {
                Debug.LogError("Prefab not found. Make sure to provide the correct path to your prefab.");
            }
        }
    }
}
