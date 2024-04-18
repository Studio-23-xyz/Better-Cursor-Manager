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
        private CursorData _cursorData;

        void Awake()
        {
            _iconHolder = GetComponent<Image>();
        }

        internal void Initialize(CursorData cursorData)
        {
            _cursorData = cursorData;
            StopAllCoroutines();
            StartCoroutine(UpdateCursorTextures(cursorData.CursorTextures, cursorData.TextureUpdateDelay));
        }

        private IEnumerator UpdateCursorTextures(Sprite[] sprites, float updateDelay)
        {
            _cursorIndex = 0;
            _iconHolder.sprite = sprites[_cursorIndex];
            while (sprites.Length > 1)
            {
                yield return new WaitForSeconds(updateDelay);

                _cursorIndex++;

                if (_cursorIndex >= sprites.Length)
                {
                    _cursorIndex = 0;
                }
                _iconHolder.sprite = sprites[_cursorIndex];
            }
        }

        void Update()
        {
            Debug.LogError($"Image Name {_iconHolder.sprite}");
        }
    }
}