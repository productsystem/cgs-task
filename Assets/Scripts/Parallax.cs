using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float length;

    public Transform player;
    public float parallaxSpeed = 0.5f;

    private Vector3 prevPos;

    void Start()
    {
        prevPos = player.position;
    }

    void Update()
    {
        transform.position += new Vector3((player.position - prevPos).x * parallaxSpeed,0,0);
        prevPos = player.position;
    }
}
