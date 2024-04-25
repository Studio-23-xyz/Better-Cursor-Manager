
using Studio23.SS2.BetterCursor.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

        public UnityEvent OnDeviceChanged;
        private InputDevice _lastUsedDevice;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _eventController = GetComponentInChildren<CursorEventController>(true);
            _locoMotionController = GetComponentInChildren<CursorLocoMotionController>(true);
            _animationController = GetComponentInChildren<CursorAnimationController>(true);
            
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Cursor.visible = false;

            ChangeCursor(CurrentCursor);
            ChangeCursorLockState(false);
            SetupLastUsedDevice();
        }



        private void Initialize()
        {
            if (CurrentCursor == null) CurrentCursor = Resources.Load<CursorData>("BetterCursor/DefaultCursor");

            _locoMotionController.Initialize(_canvas, CurrentCursor);
            _animationController.Initialize(CurrentCursor);
            _eventController.Initialize(CurrentCursor.HoverMask, CurrentCursor.SphereCastRadius);
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

        public bool IsController()
        {
            return !(_lastUsedDevice is Keyboard || _lastUsedDevice is Mouse);
        }

        private void SetupLastUsedDevice()
        {
            InputSystem.onActionChange += (obj, change) =>
            {
                if (change == InputActionChange.ActionPerformed)
                {
                    var inputAction = (InputAction)obj;
                    var lastControl = inputAction.activeControl;
                    _lastUsedDevice = lastControl.device;
                    OnDeviceChanged?.Invoke();
                }
            };
        }

        public Vector2 GetCursorImagePosition()
        {
            return _locoMotionController.GetCursorImagePosition();
        }

        public void UpdateCursorPosition(Vector2 cursorPos)
        {
            _locoMotionController.UpdateCursorPosition(cursorPos);
        }
    }
}