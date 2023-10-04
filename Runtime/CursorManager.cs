using Studio23.SS2.BetterCursorManager.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Studio23.SS2.BetterCursorManager.Core
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;
        public CursorData DefaultCursor;
        public RectTransform CursorRectTransform;
        public Canvas Canvas;


        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            Instance = this;
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            Cursor.visible = false;
            SetCursor(DefaultCursor);
            ChangeCursorLockState(false);
        }

        private void FixedUpdate()
        {
            UpdateCursorPosition();
        }

        private void UpdateCursorPosition()
        {
            // Get the mouse position in screen space.
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            // Convert the screen space mouse position to the local position of the canvas.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Canvas.transform as RectTransform,
                mousePosition,
                Canvas.worldCamera,
                out Vector2 localMousePosition
            );

            // Set the UI Image's position to follow the mouse.
            CursorRectTransform.localPosition = localMousePosition;

        }

        private void SetCursor(CursorData cursorData)
        {
            if (cursorData == null) return;
            CursorRectTransform.GetComponent<Image>().sprite = cursorData.CursorTexture;
            CursorRectTransform.sizeDelta = cursorData.PixelSize;
            CursorRectTransform.pivot = cursorData.HotSpot;
            UpdateCursorPosition();
        }

        public void ToggleCursor(bool state)
        {
            CursorRectTransform.gameObject.SetActive(state);
        }

        public void ChangeCursorLockState(bool inGame)
        {
            if (inGame)
                Cursor.lockState = CursorLockMode.Locked;
            else
            {
                #if UNITY_STANDALONE
                        // Code for standalone platforms
                        Cursor.lockState = CursorLockMode.Confined;
                #elif UNITY_WSA
                        // Code for Windows Store (UWP)
                        Cursor.lockState = CursorLockMode.None;
                #endif
            }
        }
    }
}