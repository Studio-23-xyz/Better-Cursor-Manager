using Studio23.SS2.BetterCursorManager.Data;
using UnityEngine;
using UnityEditor;

public class CrosshairCursorDataEditorWindow : EditorWindow
{
    private Sprite _crosshairTexture;
    private Texture _titleImage;

    [MenuItem("Studio-23/BetterCursor/Create Crosshair Cursor", false, 2)]
    public static void ShowWindow()
    {
        GetWindow<CrosshairCursorDataEditorWindow>("Create Crosshair Cursor");
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

        GUILayout.Label("Crosshair Cursor Data Creation", EditorStyles.boldLabel);

        _crosshairTexture =
            (Sprite)EditorGUILayout.ObjectField("Crosshair Texture", _crosshairTexture, typeof(Sprite), false);

        if (GUILayout.Button("Create Crosshair Cursor Data")) CreateCrosshairCursorDataAsset();
    }

    private void CreateCrosshairCursorDataAsset()
    {
        if (_crosshairTexture == null)
        {
            // Show an error message in the editor GUI
            EditorUtility.DisplayDialog("Error", "Crosshair Texture must be set.", "OK");
            return;
        }

        var crosshairData = CreateInstance<CrosshairCursorData>();
        crosshairData.CrosshairSprite = _crosshairTexture;

        var path = AssetDatabase.GenerateUniqueAssetPath(
            $"Assets/Packages/com.studio23.ss2.bettercursormanager/Resources/{_crosshairTexture.name}_CrosshairCursorData.asset");
        AssetDatabase.CreateAsset(crosshairData, path);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = crosshairData;
    }
}
