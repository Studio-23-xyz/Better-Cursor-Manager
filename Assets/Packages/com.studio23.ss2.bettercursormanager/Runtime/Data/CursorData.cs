using UnityEngine;

namespace Studio23.SS2.BetterCursor.Data
{
    [CreateAssetMenu(fileName = "Better Cursor", menuName = "Better Cursor/New Cursor")]
    public class CursorData : ScriptableObject
    {
        public string CursorName;
        public Sprite[] CursorTextures;
        public Vector2 HotSpot = Vector2.zero;
        public Vector2 PixelSize = new(32, 32);

        public float TextureUpdateDelay=0.1f;
    }
}