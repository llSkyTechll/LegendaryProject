using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character {
    //GameObject player;
    public float speed = 1;
    CharacterController cc;
    //Rigidbody rbd;
    //AudioSource audioSource;
    //private Animator animator;
    // Use this for initialization
    void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        //rbd = GetComponent<Rigidbody>();
        //audioSource = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();
        SetLife(100);
        health = GetComponent<Health>();
        cc = GetComponent<CharacterController>();
        soundplayer = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (health.currentHealth <= 0)
        {
            gameObject.GetComponent<Character>().Die();
        }
        Footsteps();
    }

    public override void Die()
    {
        //animator.SetTrigger("Death");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void OnDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public override void Footsteps()
    {
        if(cc.velocity.magnitude > 0.5f && cc.velocity.magnitude < 5f && soundplayer.isPlaying == false && GetComponent<movementplayer>().isGrounded)
        {
            soundplayer.clip = step;
            soundplayer.pitch = Random.Range(0.8f, 1);
            soundplayer.volume = Random.Range(0.8f, 1.1f);
            soundplayer.Play();
        }
        else if(soundplayer.isPlaying == false && cc.velocity.magnitude > 5f && GetComponent<movementplayer>().isGrounded)
        {
            soundplayer.clip = step;
            soundplayer.pitch = Random.Range(1.3f, 1.5f);
            soundplayer.volume = Random.Range(0.8f, 1.1f);
            soundplayer.Play();
        }
    }
}
