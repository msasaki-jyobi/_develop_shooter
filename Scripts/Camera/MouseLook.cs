using UnityEngine;
using UnityEngine.InputSystem;

namespace develop_shooter
{

    public class MouseLook : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [Tooltip("Sensitivity of mouse movement.")]
        public float mouseSensitivity = 100.0f;

        [Tooltip("Minimum and maximum up/down rotation angle the camera can have.")]
        public float minYAngle = -90.0f;
        public float maxYAngle = 90.0f;

        [Tooltip("Target object to rotate.")]
        public Transform target;

        private float xRotation = 0f;

        // InputAction for mouse look
        private InputAction lookAction;

        private void Awake()
        {
            // Initialize the input action
            lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");

            // Enable the input action
            lookAction.Enable();
        }

        void Start()
        {
            // Hide and lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            // Get mouse input using Input System
            Vector2 mouseDelta = lookAction.ReadValue<Vector2>();
            float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

            // Rotate target around the Y-axis (horizontal rotation)
            target.Rotate(Vector3.up * mouseX);

            // Accumulate vertical rotation and clamp it
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minYAngle, maxYAngle);

            // Apply the vertical rotation to the target (local X-axis rotation)
            target.localRotation = Quaternion.Euler(xRotation, target.localEulerAngles.y, 0.0f);
        }

        private void OnDestroy()
        {
            // Disable the input action when the object is destroyed
            lookAction.Disable();
        }
    }
}
