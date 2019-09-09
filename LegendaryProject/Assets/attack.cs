using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {
    [SerializeField]
    private Color HitColor = Color.red;
    [SerializeField]
    private float MaxDistance = 10f;
    private bool axisInUse = false;
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Fire1")!=0)
        {
            if (axisInUse == false)
            {
                axisInUse = true;
                animator.SetTrigger("Attacking");
                Component Sword = gameObject.transform.GetChild(1);
                Sword.transform.rotation = new Quaternion(Sword.transform.rotation.x + 1f, Sword.transform.rotation.y, Sword.transform.rotation.z, Sword.transform.rotation.w);
                RaycastSingle();
            }
        }
        if (Input.GetAxis("Fire1") == 0)
        {
            axisInUse = false;
        }
	}

    private void RaycastSingle()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * MaxDistance, Color.green);
        int layerMask = 1 << 9;
        RaycastHit hitinfo ;
        if (Physics.Raycast(origin,direction,out hitinfo,MaxDistance,layerMask, QueryTriggerInteraction.UseGlobal))
        {
            hitinfo.collider.GetComponent<Renderer>().material.color = HitColor;
        }
    }
}
