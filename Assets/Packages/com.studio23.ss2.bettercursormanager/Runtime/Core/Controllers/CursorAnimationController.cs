using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        private CancellationTokenSource _updateCursorCancel;

        void Awake()
        {
            _iconHolder = GetComponent<Image>();
            _updateCursorCancel = new CancellationTokenSource();
        }

        internal void Initialize(CursorData cursorData)
        {
            _cursorData = cursorData;
            _updateCursorCancel?.Cancel();
            _updateCursorCancel = new CancellationTokenSource();
            UpdateCursorTextures(cursorData.CursorTextures, cursorData.TextureUpdateDelay);
        }

        private async void UpdateCursorTextures(Sprite[] sprites, float updateDelay)
        {
            _cursorIndex = 0;
            _iconHolder.sprite = sprites[_cursorIndex];
            while (!_updateCursorCancel.IsCancellationRequested)
            {
                await UniTask.WaitForFixedUpdate();
                _cursorIndex++;
                if (_cursorIndex >= sprites.Length)
                {
                    _cursorIndex = 0;
                }
                _iconHolder.sprite = sprites[_cursorIndex];
            }
        }
    }
}