using System;
using Studio23.SS2.BetterCursor.Data;
using UnityEngine;

namespace Studio23.SS2.BetterCursor.Core
{
    public class BetterCursor : MonoBehaviour
    {
        public static BetterCursor Instance;

        [SerializeField] private Canvas _canvas;
        public CursorData CurrentCursor;

        public bool UiOnHoverEnabled;

        private CursorAnimationController _animationController;
        private CursorLocoMotionController _locoMotionController;
        private CursorEventController _eventController;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Cursor.visible = false;
            _canvas = GetComponent<Canvas>();
            _eventController = GetComponentInChildren<CursorEventController>();
            _locoMotionController = GetComponentInChildren<CursorLocoMotionController>();
            _animationController = GetComponentInChildren<CursorAnimationController>();
            Initialize();
            ChangeCursor(CurrentCursor);
            ChangeCursorLockState(false);
        }


        private void Initialize()
        {
            if (CurrentCursor == null) CurrentCursor = Resources.Load<CursorData>("BetterCursor/DefaultCursor");

            _locoMotionController.Initialize(_canvas, CurrentCursor);
            _animationController.Initialize(CurrentCursor);
            _eventController.Initialize(CurrentCursor.HoverMask);
        }


        /// <summary>
        ///     This method will set the data according to the CursorData Scriptable Class. Calling this method will change the
        ///     overall data of the cursor
        /// </summary>
        /// <param name="cursorData"></param>
        public void ChangeCursor(CursorData cursorData)
        {
            if (cursorData == null)
            {
                Debug.LogError("You attempted to initialize with null cursor data", this);
                return;
            }

            CurrentCursor = cursorData;
            Initialize();
        }


        /// <summary>
        ///  Sets cursor visibility and lockstate based on passed parameter
        /// </summary>
        /// <param name="isLocked"></param>
        public void SetCursorState(bool isLocked)
        {
            SetCursorVisibilityState(isLocked);
            ChangeCursorLockState(!isLocked);
        }


        /// <summary>
        ///     Enable or disable Cursor image
        /// </summary>
        /// <param name="state"></param>
        public void SetCursorVisibilityState(bool state)
        {
            _animationController.gameObject.SetActive(state);
        }


        /// <summary>
        ///     This method can be used to change the cursor lock state.
        /// </summary>
        /// <param name="isLocked"></param>
        public void ChangeCursorLockState(bool isLocked)
        {
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.Confined;
        }
    }
}