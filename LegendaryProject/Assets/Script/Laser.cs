using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public float laserDistance ;
    public Transform startPoint;
    public Transform endPoint;

    private LineRenderer laserLine;

    // Use this for initialization
    [System.Obsolete]
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        laserLine.SetPosition(0,startPoint.position);
        laserLine.SetPosition(1,endPoint.position);
        


        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            print("set");
            if (hit.collider)
            {
                laserLine.SetPosition(1, hit.point);
                print("hit");
            }
        }
        //else
        //{
        //    laserLine.SetPosition(1, transform.forward * laserDistance);
        //}


    }
}
