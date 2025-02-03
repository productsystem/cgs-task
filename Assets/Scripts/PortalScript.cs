using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    private HashSet<GameObject> portalEnters = new HashSet<GameObject>();

    public Transform dest;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(portalEnters.Contains(other.gameObject))
            return;
        if(dest.TryGetComponent(out PortalScript destPortal))
        {
            destPortal.portalEnters.Add(other.gameObject);
        }
        other.transform.position = dest.transform.position;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        portalEnters.Remove(other.gameObject);
    }
}
