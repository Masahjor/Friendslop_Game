using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [SerializeField] Transform cam;
    public Transform targetObject;

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
        LeftEye.LookAt(targetObject);
        RightEye.LookAt(targetObject);

        Head.LookAt(targetObject);
        //Chest.LookAt(targetObject);

        Body.rotation = Quaternion.Euler(0, yRotation, 0);

    }

}
