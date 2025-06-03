using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    float VelocityX = 0.0f;
    float VelocityY = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backwardPressed = Input.GetKey("s");

        if (forwardPressed) 
        {
            VelocityX += Time.deltaTime * acceleration;
        }
        if (backwardPressed)
        {
            VelocityX -= Time.deltaTime * acceleration;
        }
        if (leftPressed) 
        {
            VelocityY -= Time.deltaTime * acceleration;
        }
        if (rightPressed)
        {
            VelocityY += Time.deltaTime * acceleration;
        }

        animator.SetFloat("Velocity X", VelocityX);
        animator.SetFloat("Velocity Y", VelocityY);
    }

}
