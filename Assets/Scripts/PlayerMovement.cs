using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float playerHealth = 3f;
    public TextMeshProUGUI healthText;
    public float invincibleTime = 1f;
    public float jumpGrav = 2f;
    public float fallGrav = 3f;
    public float collisionCheckRay = 2f;
    public bool isPaused = false;


    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private bool isGrounded = false;
    private Vector2 moveDir = Vector2.zero;
    private bool harmable = true;

    void Start()
    {
        harmable = true;
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
        playerInput.Movement.Pause.performed += paused =>
        {
            if(!isPaused)
            {
                GameObject.FindObjectOfType<GameManager>().PauseMenuOpen();
                Time.timeScale = 0;
                isPaused = true;
            }
            if(isPaused)
            {
                GameObject.FindObjectOfType<GameManager>().PauseMenuClose();
                Time.timeScale = 1;
                isPaused = true;
            }
        };
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        healthText.text = "Health : " + playerHealth;
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = fallGrav;
        }
        else
        {
            rb.gravityScale = jumpGrav;
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
        RaycastHit2D ray = Physics2D.Raycast(transform.position,Vector2.down,collisionCheckRay , groundLayer);
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

    IEnumerator Invincible()
    {
        harmable = false;
        yield return new WaitForSeconds(invincibleTime);
        harmable = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hazard"))
        {
            if(harmable)
            {
                playerHealth--;
                StartCoroutine(Invincible());
            }
        }
        if(other.gameObject.name == "BossRoomTrigger")
        {
            CinemachineVirtualCamera cam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            cam.LookAt = other.gameObject.transform;
            cam.Follow = other.gameObject.transform;
            cam.m_Lens.OrthographicSize = 10f;
        }

        if(other.CompareTag("Goal"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
