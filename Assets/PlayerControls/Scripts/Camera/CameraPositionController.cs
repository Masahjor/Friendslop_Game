using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    public Transform cameraTransform;
    //Set it to whatever value you think is best
    public float distanceFromCamera;

    void Update()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = resultingPosition;
    }
}
