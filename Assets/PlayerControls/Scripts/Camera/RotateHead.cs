using UnityEngine;

public class RotateHead : MonoBehaviour
{
    public Transform Head, targetObject;
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
        Head.LookAt(targetObject);
    }
}
