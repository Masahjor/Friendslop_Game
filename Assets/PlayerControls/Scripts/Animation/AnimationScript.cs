using Unity.Hierarchy;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    public float VelocityZ = 0.0f;
    public float VelocityX = 0.0f;

    public float acceleration = 2.0f;
    public float deceleration = 2.0f;

    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;


    //Increase performance
    int VelocityZHash;
    int VelocityXHash;

    private void Start()
    {
        animator = GetComponent<Animator>();

        //Increase performance
        VelocityZHash = Animator.StringToHash("VelocityZ");
        VelocityXHash = Animator.StringToHash("VelocityX");

    }
    private void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        //Reset Forward/backward speed
        if (!forwardPressed && !backwardPressed && VelocityZ != 0.0f && (VelocityZ > -0.0f && VelocityZ < 0.05f))
        {
            VelocityZ = 0.0f;
        }
        //Reset left/right speed
        if (!leftPressed == !rightPressed && VelocityX != 0.0f && (VelocityX > -0.05f && VelocityX < 0.05f))
        {
            VelocityX = 0.0f;
        }

        if (forwardPressed && runPressed && VelocityZ > currentMaxVelocity)
        {
            VelocityZ = currentMaxVelocity;
        }

        //Decelerate to maximum forward walk velocity
        else if (forwardPressed && VelocityZ > currentMaxVelocity)
        {
            VelocityZ -= Time.deltaTime * deceleration;
            if (VelocityZ > currentMaxVelocity && VelocityZ < (currentMaxVelocity + 0.05f))
            {
                VelocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && VelocityZ < currentMaxVelocity && VelocityZ > (currentMaxVelocity - 0.05f))
        {
            VelocityZ = currentMaxVelocity;
        }

        //Decelerate to maximum backward walk velocity
        if (backwardPressed && VelocityZ < -currentMaxVelocity)
        {
            VelocityZ += Time.deltaTime * deceleration;
            if (VelocityZ < -currentMaxVelocity && VelocityZ > (-currentMaxVelocity + 0.05f))
            {
                VelocityZ = -currentMaxVelocity;
            }
        }

        else if (backwardPressed && VelocityZ > -currentMaxVelocity && VelocityZ < (-currentMaxVelocity + 0.05f))
        {
            VelocityZ = -currentMaxVelocity;
        }

        //Decelerate to maximum right walk velocity
        if (rightPressed && VelocityX > currentMaxVelocity)
        {
            VelocityZ -= Time.deltaTime * deceleration;
            if (VelocityX > currentMaxVelocity && VelocityX < (currentMaxVelocity + 0.05f))
            {
                VelocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && VelocityX < currentMaxVelocity && VelocityX > (currentMaxVelocity - 0.05f))
        {
            VelocityX = currentMaxVelocity;
        }

        //Decelerate to maximum left walk velocity
        if (leftPressed && VelocityX < -currentMaxVelocity)
        {
            VelocityX += Time.deltaTime * deceleration;
            if (VelocityX < -currentMaxVelocity && VelocityX > (-currentMaxVelocity + 0.05f))
            {
                VelocityX = -currentMaxVelocity;
            }
        }

        else if (leftPressed && VelocityX > -currentMaxVelocity && VelocityX < (-currentMaxVelocity + 0.05f))
        {
            VelocityX = -currentMaxVelocity;
        }

        // Increase velocity forward
        if (forwardPressed && VelocityZ < currentMaxVelocity)
        {
            VelocityZ += Time.deltaTime * acceleration;
        }
        // Increase velocity backward
        if (backwardPressed && VelocityZ > -currentMaxVelocity)
        {
            VelocityZ -= Time.deltaTime * acceleration;
        }
        // Increase velocity left
        if (leftPressed && VelocityX > -currentMaxVelocity)
        {
            VelocityX -= Time.deltaTime * acceleration;
        }
        // Increase velocity right
        if (rightPressed && VelocityX < currentMaxVelocity)
        {
            VelocityX += Time.deltaTime * acceleration;
        }

        //Decrease velocity for forward
        if (!forwardPressed && VelocityZ > 0.0f)
        {
            VelocityZ -= Time.deltaTime * deceleration;
        }
        //Decrease velocity for left
        if (!leftPressed && VelocityX < 0.0f)
        {
            VelocityX += Time.deltaTime * deceleration;
        }
        //Decrease velocity for right
        if (!rightPressed && VelocityX > 0.0f)
        {
            VelocityX -= Time.deltaTime * deceleration;
        }
        //Decrease velocity for back
        if (!backwardPressed && VelocityZ < 0.0f)
        {
            VelocityZ += Time.deltaTime * deceleration;
        }

        //Set parameters to local variables
        animator.SetFloat(VelocityZHash, VelocityZ);
        animator.SetFloat(VelocityXHash, VelocityX);
    }

}
