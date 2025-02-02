using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public GameObject[] portals;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity += new Vector2(0, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if(other.collider.CompareTag("Portal"))
        {
            GameObject otherPortal;
            if(other.collider.gameObject == portals[0])
            {
                otherPortal = portals[1];
            }
            else
            {
                otherPortal = portals[0];
            }
            rb.position = (Vector2)otherPortal.transform.position + new Vector2((Input.GetAxisRaw("Horizontal") >= 0)?1f:-1f, 0f);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
