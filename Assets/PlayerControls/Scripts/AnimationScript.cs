using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int VelocityHash;
    void Start()
    {
        // Set reference for animation
        animator = GetComponent<Animator>();

        // increase performance
        VelocityHash = Animator.StringToHash("Velocity");
    }

    
    void Update()
    {
        // Get key input from player
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed && velocity < 1)
        {
            velocity += Time.deltaTime * acceleration; 
        }
        if (!forwardPressed && velocity > 0.0f) 
        {
            velocity -= Time.deltaTime * deceleration;
        }
        if (!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }

        animator.SetFloat(VelocityHash, velocity);
    }
}
