using Studio23.SS2.BetterCursor.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Studio23.SS2.BetterCursor.Core
{
    public class CursorLocoMotionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _cursorTransform;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _cursorSpeed = 1000f;
        public InputActionAsset CursorActionAsset;

        private Vector2 _minScreenBounds;
        private Vector2 _maxScreenBounds;



        private void Awake()
        {
            _cursorTransform = GetComponent<RectTransform>();
        }

        internal void Initialize(Canvas canvas, CursorData cursorData)
        {
            _canvas = canvas;
            _cursorTransform.sizeDelta = cursorData.PixelSize;
            _cursorTransform.pivot = cursorData.HotSpot;
        }

        private void Start()
        {
            SetupBounds();
        }

        private void Update()
        {
            UpdateCursorPosition();
        }


        public Vector2 GetCursorImagePosition()
        {
            return _cursorTransform.anchoredPosition;
        }

        private void UpdateCursorPosition()
        {
            var cursorPosition = CursorActionAsset["CursorPosition"].ReadValue<Vector2>();
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
            var canvas = _cursorTransform.GetComponentInParent<Canvas>();
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            var screenSizeInCanvas = new Vector2(screenWidth, screenHeight) / canvas.scaleFactor;
            var uiElementSize = _cursorTransform.sizeDelta / 2.0f;
            _minScreenBounds = new Vector2(uiElementSize.x / 2, uiElementSize.y / 2);
            _maxScreenBounds = screenSizeInCanvas;
        }
    }
}