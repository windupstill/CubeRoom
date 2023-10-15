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
        // ���� ������Ʈ�� Mesh Renderer�� ã�� Ȱ��ȭ�մϴ�.
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = true;
        }

        // ���� �ڽ� ������Ʈ�� ���ؼ� ��������� ȣ���մϴ�.
        for (int i = 0; i < obj.childCount; i++)
        {
            EnableAllMeshRenderers(obj.GetChild(i));
        }
    }
}
