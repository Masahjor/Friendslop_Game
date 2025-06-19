using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    [Header("Camera and Target Direction")]
    [SerializeField] Transform cam;
    public Transform targetObject;
    Quaternion rotGoal;
    Vector3 Bodydirection;
    Vector3 Chestdirection;


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

    float BodyTurnSpeed = 0.1f;
    float ChestTurnSpeed = 0.5f;
    float HeadTurnSpeed = 0.8f;

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

        Bodydirection = (targetObject.position - Body.position).normalized;
        Chestdirection = (targetObject.position - Chest.position).normalized;

        rotGoal = Quaternion.LookRotation(Bodydirection);
        rotGoal = Quaternion.LookRotation(Chestdirection);

        Body.rotation = Quaternion.Slerp(Body.rotation, rotGoal, BodyTurnSpeed);
        Body.rotation = Quaternion.Euler(0, Body.eulerAngles.y, 0);

        Chest.rotation = Quaternion.Slerp(Body.rotation, rotGoal, ChestTurnSpeed);
        Chest.rotation = Quaternion.Euler(0, Body.eulerAngles.y, 0);

    }

    private void LateUpdate()
    {


        //The Eyes
        LeftEye.localRotation = Quaternion.Euler(xEyeRotation, yEyeRotation, 0);

        RightEye.localRotation = Quaternion.Euler(xEyeRotation, yEyeRotation, 0);

        Head.rotation = Quaternion.Slerp(Head.rotation, rotGoal, HeadTurnSpeed);


        //Body



    }


}
