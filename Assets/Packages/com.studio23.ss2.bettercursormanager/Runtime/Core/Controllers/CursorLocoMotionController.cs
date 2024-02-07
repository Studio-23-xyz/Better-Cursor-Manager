using Studio23.SS2.BetterCursor.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Studio23.SS2.BetterCursor.Core
{
    public class CursorLocoMotionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _cursorTransoform;
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            _cursorTransoform = GetComponent<RectTransform>();
        }

        internal void Initialize(Canvas canvas, CursorData cursorData)
        {
            _canvas = canvas;
            _cursorTransoform.sizeDelta = cursorData.PixelSize;
            _cursorTransoform.pivot = cursorData.HotSpot;
        }

        private void Update()
        {
            UpdateCursorPosition();
        }

        private void UpdateCursorPosition()
        {
            if (_canvas == null) return;
            if (Cursor.lockState == CursorLockMode.Locked) return;


            var mousePosition = Mouse.current.position.ReadValue();

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                mousePosition,
                _canvas.worldCamera,
                out var localMousePosition
            );


            _cursorTransoform.localPosition = localMousePosition;
        }
    }
}