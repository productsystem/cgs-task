using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float playerHealth = 3f;
    public TextMeshProUGUI healthText;

    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private bool isGrounded = false;
    private Vector2 moveDir = Vector2.zero;

    void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.Movement.Move.performed += moving =>
        {
            moveDir = moving.ReadValue<Vector2>();
        };
        playerInput.Movement.Move.canceled += c => moveDir = Vector2.zero;
        playerInput.Movement.Jump.performed += jump =>
        {
            Jump();
        };
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        healthText.text = "Health : " + playerHealth;
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = 3;
        }
        else
        {
            rb.gravityScale = 2;
        }
    }

    void Jump()
    {
        if(isGrounded)
        {
            rb.velocity += new Vector2(0, jumpForce);
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
