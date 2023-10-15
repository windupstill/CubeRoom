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
            EnableAllMeshRenderers(transform);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grab"))
        {
            isMiniMapOn = false;
        }
    }

    void EnableAllMeshRenderers(Transform obj)
    {
        // 현재 오브젝트의 Mesh Renderer를 찾아 활성화합니다.
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        // 하위 자식 오브젝트에 대해서 재귀적으로 호출합니다.
        for (int i = 0; i < obj.childCount; i++)
        {
            EnableAllMeshRenderers(obj.GetChild(i));
        }
    }
}
