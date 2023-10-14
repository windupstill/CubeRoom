using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    public Camera playerCamera;
    private GameObject heldObject;

    void Start()
    {
        playerCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // ����ĳ��Ʈ�� ȭ�� �߾ӿ��� ���ϴ�.
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);
        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // �浹�� ������Ʈ�� �±װ� "Grab"�� ��쿡�� ������Ʈ�� ����ϴ�.
                if (hit.collider.CompareTag("Grab"))
                {
                    heldObject = hit.collider.gameObject;

                    heldObject.transform.parent = transform;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (heldObject != null)
                {
                    heldObject.GetComponent<Rigidbody>().useGravity = true;
                    heldObject.transform.parent = null;
                    heldObject = null;
                }
            }
        }

        if (heldObject != null)
        {
            Vector3 targetPosition = ray.GetPoint(2f);
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
