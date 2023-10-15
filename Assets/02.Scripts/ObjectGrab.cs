using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    public Camera playerCamera;
    private GameObject heldObject;
    private bool isSnapped = false;
    public Vector3 snapPosition;

    void Start()
    {
        playerCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // 레이캐스트를 화면 중앙에서 쏩니다.
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.red);
        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 충돌한 오브젝트의 태그가 "Grab"인 경우에만 오브젝트를 잡습니다.
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
            if (isSnapped)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // 다시 그랩하여 빼낼 수 있도록 처리
                    heldObject.transform.parent = null;
                    heldObject.GetComponent<Rigidbody>().useGravity = true;
                    heldObject = null;
                    isSnapped = false;
                }
            }
            else
            {
                Vector3 targetPosition = ray.GetPoint(2f);
                heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * 5f);

                // 만약 스냅 위치에 도달하면 물체를 스냅하고 isSnapped를 true로 설정
                if (Vector3.Distance(heldObject.transform.position, snapPosition) < 0.1f)
                {
                    isSnapped = true;
                }
            }
        }
    }

    // 스냅 위치를 설정하는 메서드
    public void SetSnapPosition(Vector3 position)
    {
        snapPosition = position;
    }
}

