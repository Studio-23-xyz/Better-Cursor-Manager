using Studio23.SS2.BetterCursor.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Studio23.SS2.BetterCursor.Core
{
    public class CursorLocoMotionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _cursorTransoform;
        [SerializeField] private Canvas _canvas;
        public InputActionAsset CursorActionAsset;

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
            if (CursorActionAsset != null)
            {
                if (_canvas == null) return;
                if (Cursor.lockState == CursorLockMode.Locked) return;
                var cursorPosition = CursorActionAsset["CursorPosition"].ReadValue<Vector2>();

                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvas.transform as RectTransform,
                    cursorPosition,
                    _canvas.worldCamera,
                    out var localMousePosition
                );

                _cursorTransoform.localPosition = localMousePosition;
            }
            else
            {
                Debug.LogError("Cursoractionasset not assigned");
            }
        }
    }
}