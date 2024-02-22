using Studio23.SS2.BetterCursor.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Studio23.SS2.BetterCursor.Core
{
    public class CursorLocoMotionController : MonoBehaviour
    {
        [SerializeField] private RectTransform _cursorTransoform;
        [SerializeField] private Canvas _canvas;
        public InputActionAsset CursorActionAsset;
        private Vector2 screenBounds;

        private Vector2 minScreenBounds;
        private Vector2 maxScreenBounds;
        private bool isDragging = false;


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

        void Start()
        {
            Canvas canvas = _cursorTransoform.GetComponentInParent<Canvas>();
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            Vector2 screenSizeInCanvas = new Vector2(screenWidth, screenHeight) / canvas.scaleFactor;
            Vector2 uiElementSize = _cursorTransoform.sizeDelta / 2.0f;
            minScreenBounds = new Vector2(uiElementSize.x / 2, uiElementSize.y / 2);
            maxScreenBounds = screenSizeInCanvas;
        }

        private void Update()
        {
            //UpdateControllerInput();
            //HandleMouseInput();
            var cursorPosition = CursorActionAsset["CursorPosition"].ReadValue<Vector2>();
            Debug.Log($"Input data {cursorPosition}");
        }


        private void UpdateControllerInput()
        {
            Vector2 stickInput = Gamepad.current?.leftStick.ReadValue() ?? Vector2.zero;
            Vector3 move = new Vector3(stickInput.x, stickInput.y, 0) * 1000f * Time.deltaTime;

            Vector2 nextPosition = _cursorTransoform.anchoredPosition + new Vector2(move.x, move.y);
            nextPosition.x = Mathf.Clamp(nextPosition.x, minScreenBounds.x, maxScreenBounds.x);
            nextPosition.y = Mathf.Clamp(nextPosition.y, minScreenBounds.y, maxScreenBounds.y);
            _cursorTransoform.anchoredPosition = nextPosition;
            Debug.Log("");
        }

        void HandleMouseInput()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                isDragging = true;
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
            }
            if (isDragging)
            {
                var cursorPosition = CursorActionAsset["CursorPosition"].ReadValue<Vector2>();



                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvas.transform as RectTransform,
                    cursorPosition,
                    _canvas.worldCamera,
                    out var localMousePosition
                );

                _cursorTransoform.localPosition = localMousePosition;
            }

            Debug.Log("Is Dragging :" + isDragging);
        }

    }
}