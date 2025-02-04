using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public InputAction playerControls;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Vector2 moveDir = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        moveDir = playerControls.ReadValue<Vector2>();
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity += new Vector2(0, jumpForce);
        }
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = 3;
        }
        else
        {
            rb.gravityScale = 2;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
        RaycastHit2D ray = Physics2D.Raycast(transform.position,Vector2.down,2f , groundLayer);
        if(ray.collider != null)
        {
            if(ray.collider.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
        
    }
}
