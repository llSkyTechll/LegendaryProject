using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {
    [SerializeField]
    private float attackRange = 2f;
    private bool axisInUse = false;
    private Animator animator;
    private CharacterController characterController;
    private Weapon equippedWeapon;
    private float attackCooldown = 0f;
    private int minDamage = 5;
    private int maxDamage = 5;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        equippedWeapon = GetComponentInChildren<Weapon>();
        if (equippedWeapon != null)
        {
            minDamage = equippedWeapon.minDamage;
            maxDamage = equippedWeapon.maxDamage;
            attackRange = equippedWeapon.range;
        }
    }
	
	// Update is called once per frame
	void Update () {
        attackCooldown -= Time.deltaTime;
        if (Input.GetAxis("Fire1")!=0 && !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>().PlayerNotFocused)
        {
            if (axisInUse == false && GetComponent<movementplayer>().isGrounded)
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

        Debug.DrawRay(origin, direction * attackRange, Color.green);
        int layerMask = 1 << 9;
        RaycastHit hitinfo ;
        if (Physics.Raycast(origin,direction,out hitinfo,attackRange,layerMask, QueryTriggerInteraction.UseGlobal))
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
