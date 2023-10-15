using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<GameObject> map = new List<GameObject>();
    void Update()
    {
        for (int i = 0; i < objects.Count; i++) {
            if (objects[i].GetComponent<MiniMap>().isMiniMapOn)
            {
                map[i].SetActive(true);
            }
            else
            {
                map[i].SetActive(false);
            }
        }
    }
}
