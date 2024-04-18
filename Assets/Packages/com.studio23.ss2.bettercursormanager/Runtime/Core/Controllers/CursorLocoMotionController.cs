using System;
using Studio23.SS2.BetterCursor.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Studio23.SS2.BetterCursor.Core
{
    public class CursorLocoMotionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _cursorTransform;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _cursorSpeed = 1000f;

        private Vector2 _cursorPosition;
        private Vector2 _minScreenBounds;
        private Vector2 _maxScreenBounds;
        private Vector2 cursorPosition;


        private void Awake()
        {
            _cursorTransform = GetComponent<RectTransform>();
        }

        internal void Initialize(Canvas canvas, CursorData cursorData)
        {
            _canvas = canvas;
            _cursorTransform.sizeDelta = cursorData.PixelSize;
            _cursorTransform.pivot = cursorData.HotSpot;
            SetupBounds();
        }

        private void Update()
        {
            UpdateCursorPosition();
        }


        internal Vector2 GetCursorImagePosition()
        {
            return _cursorTransform.anchoredPosition * _canvas.scaleFactor;
        }

        private void UpdateCursorPosition()
        {
            if (BetterCursor.Instance.IsController())
                HandleControllerInput(cursorPosition);
            else
                HandleMouseInput(cursorPosition);
        }



        private void HandleControllerInput(Vector2 position)
        {
            var move = new Vector3(position.x, position.y, 0) * _cursorSpeed * Time.deltaTime;
            var nextPosition = _cursorTransform.anchoredPosition + new Vector2(move.x, move.y);
            nextPosition.x = Mathf.Clamp(nextPosition.x, _minScreenBounds.x, _maxScreenBounds.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, _minScreenBounds.y, _maxScreenBounds.y);
            _cursorTransform.anchoredPosition = nextPosition;
        }

        private void HandleMouseInput(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                position,
                _canvas.worldCamera,
                out var localMousePosition
            );
            _cursorTransform.localPosition = localMousePosition;
        }


        private void SetupBounds()
        {
            float screenWidth = Screen.width/ _canvas.scaleFactor;
            float screenHeight = Screen.height/ _canvas.scaleFactor;
            var screenSizeInCanvas = new Vector2(screenWidth, screenHeight);
            var uiElementSize = _cursorTransform.sizeDelta / 2.0f;
            _minScreenBounds = new Vector2(uiElementSize.x / 2, uiElementSize.y / 2);
            _maxScreenBounds = screenSizeInCanvas;
        }


        public void UpdateCursorPosition(Vector2 val)
        {
            cursorPosition = val;
        }

        public Vector2 GetCursorPosition()
        {
            return _cursorPosition;
        }
    }
}