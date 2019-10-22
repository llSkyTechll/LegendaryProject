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
    private int minDamage = 1;
    private int maxDamage = 1;
    private bool isGrounded;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        equippedWeapon = GetComponentInChildren<Weapon>();
        if (equippedWeapon != null)
        {
            minDamage = equippedWeapon.minDamage;
            maxDamage = equippedWeapon.maxDamage;
        }
    }
	
	// Update is called once per frame
	void Update () {
        isGrounded = GetComponent<movementplayer>().isGrounded;
        attackCooldown -= Time.deltaTime;
        if (Input.GetAxis("Fire1")!=0)
        {
            if (axisInUse == false && isGrounded)
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
            DealRandomDamage(minDamage, maxDamage,hitinfo);
        }
    }

    private void DealRandomDamage(int lowStat,int highStat, RaycastHit hitinfo)
    {
        System.Random rnd = new System.Random();
        int damage = rnd.Next(lowStat, highStat + 1);
        hitinfo.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
    }
}
