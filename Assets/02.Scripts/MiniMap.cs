using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public bool isMiniMapOn = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grab"))
        {
            isMiniMapOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grab"))
        {
            isMiniMapOn = false;
        }
    }
}
