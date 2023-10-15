using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnapPosition : MonoBehaviour
{
    public Vector3 position; // 스냅 위치를 저장할 변수

    private void OnDrawGizmos()
    {
        position= this.transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(position, Vector3.one);
    }
}
