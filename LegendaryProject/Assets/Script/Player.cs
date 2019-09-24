using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Enemy {
    GameObject player;
    public float speed = 1;
    Rigidbody rbd;
    AudioSource audioSource;
    private Animator animator;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
        rbd = GetComponent<Rigidbody>();
        //audioSource = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();
        SetLife(100);
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    public override void Die()
    {
        //animator.SetTrigger("Death");
        print("die");
        Destroy(gameObject, 10);
    }

    public override void OnDamage(int damage)
    {
        health.TakeDamage(damage);
    }
}
