using Studio23.SS2.BetterCursorManager.Data;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.BetterCursorManager.Editor
{
    public class CursorDataEditorWindow : EditorWindow
    {
        private Sprite cursorTexture;
        private Vector2 hotspot = new Vector2(.3f, .8f);
        private Vector2 pixelSize = new Vector2(32, 32);

        [MenuItem("Studio-23/BetterCursor/Create CursorData", false, 1)]
        public static void ShowWindow()
        {
            GetWindow<CursorDataEditorWindow>("Create Cursor Data");
        }

        private void OnGUI()
        {
            GUILayout.Label("Cursor Data Creation", EditorStyles.boldLabel);

            cursorTexture = (Sprite)EditorGUILayout.ObjectField("Cursor Texture", cursorTexture, typeof(Sprite), false);
            hotspot = EditorGUILayout.Vector2Field("Hotspot", hotspot);
            pixelSize = EditorGUILayout.Vector2Field("Pixel Size", pixelSize);

            if (GUILayout.Button("Create Cursor Data"))
            {
                CreateCursorDataAsset();
            }
        }

        private void CreateCursorDataAsset()
        {
            CursorData cursorData = CreateInstance<CursorData>();
            cursorData.CursorTexture = cursorTexture;
            cursorData.HotSpot = hotspot;
            cursorData.PixelSize = pixelSize;

            string path = AssetDatabase.GenerateUniqueAssetPath(
                $"Assets/Packages/com.studio23.ss2.bettercursormanager/Resources/{cursorTexture.name}_CursorData.asset");
            AssetDatabase.CreateAsset(cursorData, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cursorData;
        }
    }
}