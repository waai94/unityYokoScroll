using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheckBase;
    [SerializeField] private float maxJumpTime = 0.2f;// 長押しでジャンプする時間の最大値
    [SerializeField] private float jumpHoldForce = 3f; // ジャンプ継続時の追加力

    [Header("Prehab Setting")]
    [SerializeField] private GameObject attackPrefab;
    // Components
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // Input Actions
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;

    private float jumpTimeCounter; // ジャンプ時間のカウンター
    private bool isJumping; // ジャンプ中かどうかのフラグ
    private float forwardDirection = 1f; // プレイヤーの向いている方向   
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        attackAction = playerInput.actions["Attack"];
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
       moveAction.canceled += OnMove;
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJumpCanceled;
        attackAction.performed += OnAttack;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        jumpAction.performed -= OnJump;
        jumpAction.canceled -= OnJumpCanceled;
        attackAction.performed -= OnAttack;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
       
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        bool testGrounded = IsGrounded();
        // Debug.Log($"Move Velocity: {rb.linearVelocity}, IsGrounded: {testGrounded}");

        if (isJumping)
        {
            JumpContinue();

        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        var device = context.control.device;
        Debug.Log($"Move Input: {moveInput}, Device: {device}");
        if (moveInput.x != 0)
        {
            forwardDirection = moveInput.x > 0 ? 1f : -1f;
        }

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpTimeCounter = 0;
            isJumping = true;
            Debug.Log("Jump Performed");
        }

    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        isJumping = false;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        // 攻撃処理をここに追加
        if (attackPrefab)
        {

            GameObject attackInstance = Instantiate(attackPrefab, transform.position, Quaternion.identity);
            BulletController controller = attackInstance.GetComponentInChildren<BulletController>();
            Debug.Log("Attack Performed");
            if (controller != null)
            {
                controller.BulletInitialize(Vector2.right * forwardDirection);
            }

        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckBase.position, Vector2.down, 0.31f);
        Debug.DrawRay(groundCheckBase.position, Vector2.down * 0.31f, Color.red);
        Debug.Log($"Ground Check Hit: {hit.collider} ");
        return hit.collider != null;
    }

    // ジャンプ継続処理
    private void JumpContinue()
    {
               if (jumpTimeCounter < maxJumpTime)
        {
            rb.AddForce(Vector2.up * jumpHoldForce, ForceMode2D.Force); // ジャンプ力を維持
            jumpTimeCounter += Time.deltaTime;
        }
        else
        {
            isJumping = false; // 最大ジャンプ時間に達したらジャンプを終了
        }
    }
}