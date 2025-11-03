using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheckBase;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        bool testGrounded = IsGrounded();
        Debug.Log(testGrounded);
        //Debug.Log("helloworld"); 
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckBase.position, Vector2.down, 0.31f);
        Debug.DrawRay(groundCheckBase.position, Vector2.down * 0.31f, Color.red);
        
        return hit.collider != null;
    }
}