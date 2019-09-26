using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTiger : Character {
    protected GameObject player;
    public float speed = 1;
    protected Rigidbody rbd;
    AudioSource audioSource;
    protected Animator animator;
    //public AudioMusic audioMusic;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rbd = GetComponent<Rigidbody>();
        //audioSource = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();
        SetLife(100);
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    //private void FollowPlayer()
    //{
    //    animator.Play("Run");
    //    rbd.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
    //}
    public override void Die()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 10);
    }

    public override void OnDamage(int damage)
    {
        health.TakeDamage(damage);
    }
}