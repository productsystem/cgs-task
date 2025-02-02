using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float yOffset = 0f;

    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, GameObject.Find("Player").transform.position, 1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
