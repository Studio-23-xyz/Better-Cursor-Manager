using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotator : MonoBehaviour
{
    public Camera MainCamera;

    public float RotationSpeed = 2.0f; // Adjust this value to control the rotation speed

    private void Update()
    {
        // Rotate the main camera based on mouse input
        float _mouseX = Mouse.current.delta.x.ReadValue();
        float _mouseY = Mouse.current.delta.y.ReadValue();
        Vector3 _rotation = new Vector3(-_mouseY, _mouseX, 0) * RotationSpeed;
        transform.Rotate(_rotation);
    }
}
