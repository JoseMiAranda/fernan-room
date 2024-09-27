using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public float mouseSensitivity = 80f;
    public Transform playerBody;
    float xRotation = 0;

    void Update()
    {
        if (GameManager.Instance.CanMove())
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90); // Avoid total rotation in Axis Y

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
