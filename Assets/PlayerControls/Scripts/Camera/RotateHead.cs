using UnityEngine;

public class RotateHead : MonoBehaviour
{
    float xRotation;
    float yRotation;

    [SerializeField] Transform Head;
    [SerializeField] Transform Chest;
    [SerializeField] Transform Body;


    public Transform targetObject;
    
    public Transform LeftEye;
    public Transform RightEye;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //The Eyes
        LeftEye.LookAt(targetObject);
        RightEye.LookAt(targetObject);

        Head.LookAt(targetObject);
        //Chest.LookAt(targetObject);


    }
}
