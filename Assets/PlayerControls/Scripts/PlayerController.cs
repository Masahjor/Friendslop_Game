using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerHeight = 1f;

    [Header("Movement")]
    public float moveSpeed = 6.0f;
    public float movementMultiplier = 5f;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Jumping")]
    public float jumpForce = 75f;
    public float fallMultiplier = 2.0f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6.0f;
    float airDrag = 2.0f;


    float horizontalMovement;
    float verticalMovement;

    bool isGrounded;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2);

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

        if (rb.angularVelocity.y < 0)
        {
            rb.angularVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    void MyInput() 
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
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
