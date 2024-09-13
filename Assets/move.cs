using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    MapBehaviour mapBehaviour;
    bool reachEnd = false;
    bool isStarted = false;
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("start");
        mapBehaviour = FindObjectOfType<MapBehaviour>();
        Debug.Log(mapBehaviour.aa.Count);
        //StartCoroutine(FindWayPoint(mapBehaviour.aa));
    }

    IEnumerator FindWayPoint(List<Vector3> path)
    {
        foreach (Vector3 v in mapBehaviour.aa)
            {
                //transform.position = mapBehaviour.tilemap.WorldToCell(v);
                transform.position = new Vector3(v.x + 0.5f, v.y+0.5f, 0);
                yield return new WaitForSeconds(0.01f);
                //Debug.Log(transform.position.ToString());
                if (transform.position == mapBehaviour.endPointaa) {
                    //reachEnd = true;
                    StopCoroutine(FindWayPoint(mapBehaviour.aa));
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (mapBehaviour.aa.Count > 0 && !isStarted) {
            isStarted = true;
            StartCoroutine(FindWayPoint(mapBehaviour.aa));
        }
    }
}
