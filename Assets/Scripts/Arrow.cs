using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    public ParticleSystem hitParticle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
