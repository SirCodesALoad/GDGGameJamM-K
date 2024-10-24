using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Moveable : MonoBehaviour
{
    public bool Loop = false, finished = false;
    public float Speed = 0.5f;
    public Transform[] WayPoints = {};
    private int CurrWayPointIndex = 0;
    
    void Update()
    {
        if (!finished)
        {
            var step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, WayPoints[CurrWayPointIndex].position, step);
            
            if (Vector3.Distance(transform.position, WayPoints[CurrWayPointIndex].position) < 0.001f)
            {
                CurrWayPointIndex++;
                if (CurrWayPointIndex >= WayPoints.Length)
                {
                    CurrWayPointIndex = 0;
                    if (!Loop)
                    {
                        finished = true;
                        Object.Destroy(this);
                    }
                }
            }
        }
    }
}
