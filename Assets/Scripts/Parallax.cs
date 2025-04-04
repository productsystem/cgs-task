using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Vector2 startpos;
    public Transform cam = null;
    public float parallaxEffect = 0.0f;
    void Start()
    {
        startpos = transform.position;
    }

    void Update()
    {
        Vector2 dist = new Vector2(cam.transform.position.x * parallaxEffect, 0);
        transform.position = startpos + dist;
    }
}
