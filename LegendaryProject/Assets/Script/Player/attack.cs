using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {
    [SerializeField]
    private float MaxDistance = 2f;
    private bool axisInUse = false;
    private Animator animator;
    private CharacterController characterController;
    private Weapon equippedWeapon;
    private float attackCooldown = 0f;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        equippedWeapon = GetComponentInChildren<Weapon>();
    }
	
	// Update is called once per frame
	void Update () {
        attackCooldown -= Time.deltaTime;
        if (Input.GetAxis("Fire1")!=0)
        {
            if (axisInUse == false && characterController.isGrounded)
            {
                if (attackCooldown <= 0f)
                {
                    axisInUse = true;
                    animator.SetTrigger("Attacking");
                    RaycastSingle();
                    attackCooldown = 1.5f;
                } 
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
            DealRandomDamage(equippedWeapon.minDamage, equippedWeapon.maxDamage,hitinfo);
        }
    }

    private void DealRandomDamage(int lowStat,int highStat, RaycastHit hitinfo)
    {
        System.Random rnd = new System.Random();
        int damage = rnd.Next(lowStat, highStat + 1);
        hitinfo.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
    }
}
