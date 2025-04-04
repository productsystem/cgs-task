using UnityEngine;

public class PortalPlacer : MonoBehaviour
{
    public GameObject greenPortal;
    public GameObject purplePortal;
    public GameObject previewPortalGreen;
    public GameObject previewPortalPurple;

    public float previewDist = 3f;
    public LayerMask whatIsObstacle;

    private GameObject currentPrev;
    private bool isGreen = true;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ShowPrev();
        }
        if(Input.GetKey(KeyCode.F))
        {
            UpdatePrev();
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            PlacePortal();
            HidePrev();
        }
    }

    void ShowPrev()
    {
        GameObject previewPortal = isGreen ? previewPortalGreen : previewPortalPurple;
        currentPrev = Instantiate(previewPortal);
    }

    void UpdatePrev()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, previewDist, whatIsObstacle);
        float dist = previewDist;
        if(hit.collider != null)
        {
            dist = hit.distance - 0.1f;
            Debug.Log("reduce");
        }
        currentPrev.transform.position = transform.position + transform.right * dist;
        currentPrev.transform.rotation = Quaternion.identity;
    }

    void HidePrev()
    {
        Destroy(currentPrev);
    }

    void PlacePortal()
    {
        GameObject spawnPort = isGreen ? greenPortal : purplePortal;
        var prevSamePortal = GameObject.FindWithTag(isGreen ? "PortalGreen" : "PortalPurple");
        if(prevSamePortal != null)
        {
            Destroy(prevSamePortal);
        }
        Instantiate(spawnPort, currentPrev.transform.position, Quaternion.identity);

        isGreen = !isGreen;
    }
}
