using UnityEngine;

namespace Studio23.SS2.BetterCursorManager.Data
{
    [CreateAssetMenu(fileName = "Better Cursor", menuName = "Better Cursor/New Cursor")]
    public class CursorData : ScriptableObject
    {
        public Sprite CursorTexture;
        public Vector2 HotSpot = Vector2.zero;
        public Vector2 PixelSize = new(32, 32);
        public float PixelScale = 1 / 6f;
    }
}