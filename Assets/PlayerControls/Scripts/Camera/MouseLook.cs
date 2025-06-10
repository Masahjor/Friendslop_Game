using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [Header("Camera and Target Direction")]
    [SerializeField] Transform cam;
    public Transform targetObject;

    [Header("Body Parts")]
    [SerializeField] Transform Head;
    [SerializeField] Transform Chest;
    [SerializeField] Transform Body;

    [Header("Eyes")]
    [SerializeField] Transform LeftEye;
    [SerializeField] Transform RightEye;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    float xEyeRotation;
    float yEyeRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -70, 70);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void LateUpdate()
    {
        //The Eyes
        LeftEye.localRotation = Quaternion.Euler(xEyeRotation, yEyeRotation, 0);

        RightEye.localRotation = Quaternion.Euler(xEyeRotation, yEyeRotation, 0);

        Head.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        Body.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
