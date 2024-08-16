using System;
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

        void OnDisable()
        {
            _updateCursorCancel?.Cancel();
            _updateCursorCancel?.Dispose();
        }

        internal async void Initialize(CursorData cursorData)
        {
            _cursorData = cursorData;
            _updateCursorCancel?.Cancel();
            _updateCursorCancel = new CancellationTokenSource();
            await UpdateCursorTextures(cursorData.CursorTextures, cursorData.TextureUpdateDelay);
        }

        private async UniTask UpdateCursorTextures(Sprite[] sprites, float updateDelay)
        {
            _cursorIndex = 0;
            _iconHolder.sprite = sprites[_cursorIndex];
            while (!_updateCursorCancel.IsCancellationRequested)
            {
                bool cancellationThrow = await UniTask.Delay(TimeSpan.FromSeconds(updateDelay),ignoreTimeScale:true, cancellationToken:_updateCursorCancel.Token).SuppressCancellationThrow();
                if(cancellationThrow) break;
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