using Studio23.SS2.BetterCursorManager.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Studio23.SS2.BetterCursorManager.Core
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager Instance;
        [SerializeField] private CursorData _currentCursor;
        [SerializeField] private CrosshairCursorData _currentCrosshair;
        [SerializeField] private RectTransform _cursorRectTransform;
        [SerializeField] private Canvas _canvas;

        private CursorData _defaultCursor;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {

#if PLATFORM_STANDALONE
            // If no cursor data given, it will load into default cursor 
            if (_currentCursor == null || _currentCrosshair == null)
            {
                _defaultCursor = Resources.Load<CursorData>("Default Cursor");
                SetCursor(_defaultCursor);
            }

            Cursor.visible = false;
            SetCursor(_currentCursor);
            ChangeCursorLockState(false);
#endif

        }

        private void FixedUpdate()
        {
#if PLATFORM_STANDALONE
            UpdateCursorPosition();
#endif

        }

        private void UpdateCursorPosition()
        {
#if PLATFORM_STANDALONE
            if (Cursor.lockState == CursorLockMode.Locked) return;

            // Get the mouse position in screen space.
            var mousePosition = Mouse.current.position.ReadValue();

            // Convert the screen space mouse position to the local position of the canvas.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                mousePosition,
                _canvas.worldCamera,
                out var localMousePosition
            );

            // Set the UI Image's position to follow the mouse.
            _cursorRectTransform.localPosition = localMousePosition;
#endif
        }

        /// <summary>
        ///     This method will set the data according to the CursorData Scriptable Class. Calling this method will change the
        ///     overall data of the cursor
        /// </summary>
        /// <param name="cursorData"></param>
        public void SetCursor(CursorData cursorData)
        {
#if PLATFORM_STANDALONE
            if (cursorData == null) return;
            _cursorRectTransform.GetComponent<Image>().sprite = cursorData.CursorTexture;
            _cursorRectTransform.sizeDelta = cursorData.PixelSize;
            _cursorRectTransform.pivot = cursorData.HotSpot;
            UpdateCursorPosition();
#endif
        }

        /// <summary>
        ///     This method will set the data according to the CrosshairCursorData Scriptable Class. Calling this method will
        ///     change the overall data of the crosshair
        /// </summary>
        /// <param name="crosshairCursorData"></param>
        public void SetCrosshair(CrosshairCursorData crosshairCursorData)
        {
            if (crosshairCursorData == null) return;
            _cursorRectTransform.GetComponent<Image>().sprite = crosshairCursorData.CrosshairSprite;
        }

        /// <summary>
        /// Enable or disable Cursor image 
        /// </summary>
        /// <param name="state"></param>
        public void ToggleCursor(bool state)
        {
            _cursorRectTransform.gameObject.SetActive(state);
        }


        /// <summary>
        /// This method can be used to change the cursor lock state. 
        /// </summary>
        /// <param name="inGame"></param>
        public void ChangeCursorLockState(bool inGame)
        {
#if PLATFORM_STANDALONE
            Cursor.lockState = inGame ? CursorLockMode.Locked : CursorLockMode.Confined;
#endif

        }


        /// <summary>
        ///     Change default mouse Cursor mode into Crosshair mode. If crosshair or mouse cursor data not found it will load into
        ///     default mouse cursor.
        /// </summary>
        [ContextMenu("Change Crosshair")]
        public void ChangeIntoCrosshair()
        {
            // Set the cursor to the crosshair cursor texture
            //_currentCursor.CursorTexture = _currentCursor.CrosshairCursorTexture;
            SetCrosshair(_currentCrosshair);
            // Lock the cursor
            ChangeCursorLockState(true);
            _cursorRectTransform.localPosition = Vector3.zero;
        }

        /// <summary>
        ///     Change crosshair mode to mouse cursor mode. If crosshair or mouse cursor data not found it will load into default
        ///     mouse cursor.
        /// </summary>
        [ContextMenu("Change Cursor")]
        public void ChangeIntoCursor()
        {
            // Set the cursor to the crosshair cursor texture
            //_currentCursor.CursorTexture = _currentCursor.CrosshairCursorTexture;
            SetCursor(_currentCursor);
            // Lock the cursor
            ChangeCursorLockState(false);
        }
    }

}