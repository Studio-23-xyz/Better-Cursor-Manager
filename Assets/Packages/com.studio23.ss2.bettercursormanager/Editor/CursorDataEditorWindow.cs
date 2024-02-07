using System.Collections.Generic;
using System.IO;
using Studio23.SS2.BetterCursor.Data;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.BetterCursor.Editor
{
    public class CursorDataEditorWindow : EditorWindow
    {
        private string _cursorName;
        private List<Sprite> _cursorTextures=new List<Sprite>(1);
        private Vector2 _hotspot = new(.3f, .8f);
        private Vector2 _pixelSize = new(32, 32);

        private float _cursorTextureUpdateDelay=0.1f;

        private static readonly string _folderPath = "Assets/Resources/BetterCursor/";

        [MenuItem("Studio-23/BetterCursor/Create Cursor", false, 1)]
        public static void ShowWindow()
        {
            
            GetWindow<CursorDataEditorWindow>("Create Cursor");
        }


        private void OnGUI()
        {
            GUILayout.Label("Cursor Data Creation", EditorStyles.boldLabel);
            _cursorName = EditorGUILayout.TextField("Name", _cursorName);
            _cursorTextureUpdateDelay = EditorGUILayout.FloatField("Texture Update Delay", _cursorTextureUpdateDelay);
            _hotspot = EditorGUILayout.Vector2Field("Hotspot", _hotspot);
            _pixelSize = EditorGUILayout.Vector2Field("Pixel Size", _pixelSize);

            EditorGUILayout.Space();
            GUILayout.Label("Cursor Textures", EditorStyles.boldLabel);
            // Display existing sprite fields
            for (int i = 0; i < _cursorTextures.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _cursorTextures[i] = (Sprite)EditorGUILayout.ObjectField($"Texture {i + 1}", _cursorTextures[i], typeof(Sprite), true);
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _cursorTextures.RemoveAt(i);
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            // Add a default sprite field if there are no sprites
            if (_cursorTextures.Count == 0)
            {
                _cursorTextures.Add(null);
            }

            // Button to add more sprite fields
            if (GUILayout.Button("Add Sprite"))
            {
                _cursorTextures.Add(null);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Cursor Data"))
            {
                CreateCursorDataAsset();
            }
        }



        private void CreateCursorDataAsset()
        {
            

            var cursorData = CreateInstance<CursorData>();
            cursorData.CursorName = _cursorName;
            cursorData.CursorTextures = _cursorTextures.ToArray();
            cursorData.TextureUpdateDelay=_cursorTextureUpdateDelay;
            cursorData.HotSpot = _hotspot;
            cursorData.PixelSize = _pixelSize;


            if (!Directory.Exists(_folderPath)) 
                Directory.CreateDirectory(_folderPath);
            var cursorDataPath = Path.Combine(_folderPath, $"{cursorData.name}_CursorData.asset");

            AssetDatabase.CreateAsset(cursorData, cursorDataPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cursorData;
        }
    }
}