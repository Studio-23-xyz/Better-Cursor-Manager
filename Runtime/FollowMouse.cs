using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Get the mouse position in screen space.
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Convert the screen space mouse position to the local position of the canvas.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePosition,
            canvas.worldCamera,
            out Vector2 localMousePosition
        );

        // Set the UI Image's position to follow the mouse.
        rectTransform.localPosition = localMousePosition;
    }
}
