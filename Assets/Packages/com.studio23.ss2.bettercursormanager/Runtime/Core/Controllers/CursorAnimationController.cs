using System.Collections;
using Studio23.SS2.BetterCursor.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.BetterCursor.Core
{
    internal class CursorAnimationController : MonoBehaviour
    {
        [SerializeField] private Image _iconHolder;

        private int _cursorIndex;

        void Awake()
        {
            _iconHolder = GetComponent<Image>();
        }

        internal void Initialize(CursorData cursorData)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateCursorTextures(cursorData.CursorTextures, cursorData.TextureUpdateDelay));
        }

        private IEnumerator UpdateCursorTextures(Sprite[] sprites, float updateDelay)
        {
            _cursorIndex = 0;
            while (sprites.Length > 1)
            {
                yield return new WaitForSeconds(updateDelay);

                _iconHolder.sprite = sprites[_cursorIndex++];
                if (_cursorIndex == sprites.Length)
                {
                    _cursorIndex = 0;
                }
            }
        }
    }
}