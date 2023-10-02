using Studio23.SS2.BetterCursorManager.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Studio23.SS2.BetterCursorManager.Core
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;
        public CursorData DefaultCursor;
        private CursorData _currentCursor;
        private Transform _cursorTransform;
        private Camera _mainCamera;


        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            Instance = this;
        }

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            var customCursor = new GameObject("CustomCursor");
            customCursor.AddComponent<SpriteRenderer>();
            _cursorTransform = customCursor.transform;
            _mainCamera = Camera.main;

            SetCursor(DefaultCursor);
            InputSystem.onDeviceChange += HandleDeviceChange;
        }

        private void OnDestroy()
        {
            InputSystem.onDeviceChange -= HandleDeviceChange;
        }

        private void Update()
        {
            UpdateCursorPosition();
        }

        private void HandleDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Added) UpdateCursorPosition();
        }

        private void UpdateCursorPosition()
        {
            var cursorPosition = Mouse.current.position.ReadValue();
            _cursorTransform.position =
                _mainCamera.ScreenToWorldPoint(new Vector3(cursorPosition.x, cursorPosition.y, 10f));
        }

        private void SetCursor(CursorData cursorData)
        {
            if (cursorData == null) return;
            _currentCursor = cursorData;
            var cursorHotSpot = new Vector2(
                cursorData.HotSpot.x * cursorData.PixelSize.x,
                cursorData.HotSpot.y * cursorData.PixelSize.y
            );

            _cursorTransform.GetComponent<SpriteRenderer>().sprite = cursorData.CursorTexture;
            _cursorTransform.localScale =
                new Vector3(cursorData.PixelScale, cursorData.PixelScale, cursorData.PixelScale);

            UpdateCursorPosition();
        }

        public void ToggleCursor(bool state)
        {
            _cursorTransform.gameObject.SetActive(state);
        }
    }
}