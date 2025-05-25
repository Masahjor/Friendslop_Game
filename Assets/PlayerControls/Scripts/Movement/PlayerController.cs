using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerHeight = 1f;
    [SerializeField] Transform Orientation;

    [Header("Movement")]
    public float moveSpeed = 4.0f;
    public float movementMultiplier = 5f;
    [SerializeField] float airMultiplier = 0.5f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 40f;
    public float fallMultiplier = 5.0f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    float groundDrag = 6.0f;
    float airDrag = 2.0f;


    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;

    Vector3 moveDirection;
    Vector3 SlopeMoveDirection;

    Rigidbody rb;
    Animator animator;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit,playerHeight / 2 + 0.5f)) 
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded) 
        {
            Jump();
        }

        SlopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }


    private void FixedUpdate() 
    {
        MovePlayer();

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    void MyInput() 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = Orientation.forward * verticalMovement + Orientation.right * horizontalMovement;
    }

    void Jump() 
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ControlSpeed() 
    {
        if(Input.GetKey(sprintKey) && isGrounded) 
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else 
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded) 
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = airDrag;
        }
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope()) 
        {
           rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope()) 
        {
            rb.AddForce(SlopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        animator.SetFloat("VelocityX", moveSpeed);
        animator.SetFloat("VelocityY", moveSpeed);

    }
}
