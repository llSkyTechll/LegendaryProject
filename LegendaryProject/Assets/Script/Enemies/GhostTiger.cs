using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTiger : EnemyAI {
    public float speed = 1;
    protected Rigidbody rbd;
    AudioSource audioSource;
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
}