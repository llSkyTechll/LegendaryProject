using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour {
    [SerializeField]
    private float attackRange = 2f;
    private bool axisInUse = false;
    private Animator animator;
    private Weapon equippedWeapon;
    private float attackCooldown = 0f;
    private int minDamage = 5;
    private int maxDamage = 5;
    private float cooldown = 0f;
    public GameObject fireball;
    private MouvementCamera cam;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        equippedWeapon = GetComponentInChildren<Weapon>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>();
        if (equippedWeapon != null)
        {
            minDamage = equippedWeapon.minDamage;
            maxDamage = equippedWeapon.maxDamage;
            attackRange = equippedWeapon.range;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        attackCooldown -= Time.deltaTime;
        cooldown -= Time.deltaTime;
        if (CanAttack())
        {
            if (Input.GetAxis("Fire1") != 0 && axisInUse == false && GetComponent<movementplayer>().isGrounded)
            {
                if (attackCooldown <= 0f)
                {
                    axisInUse = true;
                    animator.SetTrigger("Attacking");
                    if (equippedWeapon != null)
                    {
                        equippedWeapon.Swish();
                    }
                    RaycastSingle();
                    attackCooldown = 1.5f;
                }
            }
            if(Input.GetAxis("Fire3") != 0 && cooldown <= 0)
            {
                if(fireball!=null)
                {
                    Vector3 forward = cam.camTransform.rotation.eulerAngles;
                    forward.x = forward.x-10;
                    Instantiate(fireball, transform.position+transform.forward,Quaternion.Euler(forward));
                    animator.SetTrigger("Spell");
                }
                cooldown = 3f;
            }
        }
        if (Input.GetAxis("Fire1") == 0)
        {
            axisInUse = false;
        }
        
    }

    private static bool CanAttack()
    {
        return !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>().PlayerNotFocused &&
            !GameObject.FindGameObjectWithTag("Inventory").GetComponentInChildren<KeyPressPanel>().GetInventoryIsOpen() &&
            !GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetIsDead();
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
            if (equippedWeapon != null)
            {
                equippedWeapon.Hit();
            }
        }
    }

    private void DealRandomDamage(int lowStat,int highStat, RaycastHit hitinfo)
    {
        System.Random rnd = new System.Random();
        int damage = rnd.Next(lowStat, highStat + 1);
        hitinfo.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
    }
}
