using System.IO;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer;
using Studio23.SS2.BetterCursorManager.Data;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.BetterCursorManager.Editor
{
    public class CursorDataEditorWindow : EditorWindow
    {
        private Sprite _cursorTexture;
        private Vector2 _hotspot = new(.3f, .8f);
        private Vector2 _pixelSize = new(32, 32);

        private Texture _titleImage;

        private static readonly string _folderPath = "Assets/Resources/BetterCursor/";

        [MenuItem("Studio-23/BetterCursor/Create Cursor", false, 1)]
        public static void ShowWindow()
        {
            GetWindow<CursorDataEditorWindow>("Create Cursor");
        }


        private void OnGUI()
        {
            if (_titleImage == null)
                // Load the title image from the Resources folder
                _titleImage = Resources.Load<Texture>("Images/Title");

            // Display the title image at the top of the window
            if (_titleImage != null)
            {
                var titleRect = EditorGUILayout.GetControlRect(false, _titleImage.height);
                EditorGUI.DrawPreviewTexture(titleRect, _titleImage);
            }

            GUILayout.Label("Cursor Data Creation", EditorStyles.boldLabel);

            _cursorTexture =
                (Sprite)EditorGUILayout.ObjectField("Cursor Texture", _cursorTexture, typeof(Sprite), false);
            _hotspot = EditorGUILayout.Vector2Field("Hotspot", _hotspot);
            _pixelSize = EditorGUILayout.Vector2Field("Pixel Size", _pixelSize);

            if (GUILayout.Button("Create Cursor Data")) CreateCursorDataAsset();
        }

        private void CreateCursorDataAsset()
        {
            if (_cursorTexture == null)
            {
                // Show an error message in the editor GUI
                EditorUtility.DisplayDialog("Error", "Cursor Texture and Crosshair Texture must be set.", "OK");
                return;
            }

            var cursorData = CreateInstance<CursorData>();
            cursorData.CursorTexture = _cursorTexture;
            cursorData.HotSpot = _hotspot;
            cursorData.PixelSize = _pixelSize;


            if (!Directory.Exists(_folderPath)) Directory.CreateDirectory(_folderPath);
            var cursorDataPath = Path.Combine(_folderPath, $"{_cursorTexture.name}_CursorData.asset");

            AssetDatabase.CreateAsset(cursorData, cursorDataPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cursorData;
        }
    }
}