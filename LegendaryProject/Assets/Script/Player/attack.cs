﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {
    [SerializeField]
    private float MaxDistance = 2f;
    private bool axisInUse = false;
    private Animator animator;
    private CharacterController characterController;
    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Fire1")!=0)
        {
            if (axisInUse == false && characterController.isGrounded)
            {
                axisInUse = true;
                animator.SetTrigger("Attacking");
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
            DealRandomDamage(10, 40,hitinfo);
        }
    }

    private void DealRandomDamage(int lowStat,int highStat, RaycastHit hitinfo)
    {
        System.Random rnd = new System.Random();
        hitinfo.collider.gameObject.GetComponent<Health>().TakeDamage(rnd.Next(lowStat, highStat+1));
    }
}
