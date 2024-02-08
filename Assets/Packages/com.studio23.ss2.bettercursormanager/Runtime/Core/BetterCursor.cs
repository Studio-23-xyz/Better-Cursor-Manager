using System;
using Studio23.SS2.BetterCursor.Data;
using UnityEngine;

namespace Studio23.SS2.BetterCursor.Core
{
    public class BetterCursor : MonoBehaviour
    {
        private static readonly Lazy<BetterCursor> _instance = new(() => FindObjectOfType<BetterCursor>(), true);
        public static BetterCursor Instance => _instance.Value;


        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _camera;
        public CursorData CurrentCursor;

        public bool UiOnHoverEnabled;
        [SerializeField] private LayerMask _onHoverMask;

        private CursorAnimationController _animationController;
        private CursorLocoMotionController _locoMotionController;
        private CursorEventController _eventController;

        private void Awake()
        {
            if (_instance.IsValueCreated && _instance.Value != this)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }


        private void Start()
        {
            Cursor.visible = false;

            _camera = _camera ?? Camera.main;
            if (_camera == null)
                Debug.LogError(
                    "No Camera assigned on Better Cursor.It must be assigned for it to work.Or Tag a camera as MainCamera");

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
            if (CurrentCursor == null) CurrentCursor = Resources.Load<CursorData>("Default Cursor");

            _locoMotionController.Initialize(_canvas, CurrentCursor);
            _animationController.Initialize(CurrentCursor);
            _eventController.Initialize(_onHoverMask, _camera);
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
        ///     This method can be used to change the overall cursor state
        /// </summary>
        /// <param name="state"></param>
        public void SetCursorState(bool state)
        {
            SetCursorVisibilityState(state);
            ChangeCursorLockState(!state);
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