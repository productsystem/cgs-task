using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public float playerHealth = 3f;
    public Image healthBar;
    public float invincibleTime = 1f;
    public float jumpGrav = 2f;
    public float fallGrav = 3f;
    public float collisionCheckRay = 2f;
    public GameObject pauseMenu;
    public GameManager gameManager;


    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private bool isGrounded = false;
    private Vector2 moveDir = Vector2.zero;
    private bool harmable = true;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pauseMenu = GameObject.Find("PauseMenu");
        harmable = true;

        //input subscribing
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
            gameManager.TogglePause(pauseMenu);
        };


        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            Debug.Log("huh");
        }


        gameManager.isPaused = false;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        healthBar.fillAmount = playerHealth/ 3f;
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
        if(moveDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if (moveDir.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
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
        
        if(other.gameObject.name == "BossRoomTrigger")
        {
            CinemachineVirtualCamera cam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            cam.LookAt = other.gameObject.transform;
            cam.Follow = other.gameObject.transform;
            cam.m_Lens.OrthographicSize = 10f;
        }

        if(other.CompareTag("Goal"))
        {
            gameManager.StartLevel(1);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Hazard"))
        {
            if(harmable)
            {
                playerHealth--;
                StartCoroutine(Invincible());
            }
        }
    }
}
