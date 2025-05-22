using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerHeight = 1f;

    [SerializeField] Transform Orientation;

    [Header("Movement")]
    public float moveSpeed = 6.0f;
    public float movementMultiplier = 5f;
    [SerializeField] float airMultiplier = 0.5f;

    [Header("Jumping")]
    public float jumpForce = 50f;
    public float fallMultiplier = 2.0f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6.0f;
    float airDrag = 2.0f;


    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;

    Vector3 moveDirection;

    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0,0,0), groundDistance, groundMask);

        MyInput();
        ControlDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded) 
        {
            Jump();
        }
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
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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
        if (isGrounded) 
        {
           rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded) 
        {
           rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }


}
