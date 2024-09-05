using System.Collections;
using System.Collections.Generic;
using Studio23.SS2.BetterCursor.Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActorInputController : MonoBehaviour
{
    public delegate void OnActionEvent();
    public OnActionEvent CursorPosUpdateEvent;
    public BetterCursor BetterCursor;
    private Vector2 _cursorValue;

    // Start is called before the first frame update
    void Start()
    {
        CursorPosUpdateEvent += UpdateCursorPosition;
    }

    private void UpdateCursorPosition()
    {
        BetterCursor.UpdateCursorPosition(_cursorValue);
    }
    public void OnCursorValueInfo(InputAction.CallbackContext callbackContext)
    {
        _cursorValue = callbackContext.ReadValue<Vector2>();
        CursorPosUpdateEvent?.Invoke();
    }
}
