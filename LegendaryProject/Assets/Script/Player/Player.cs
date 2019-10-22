using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character {
    //GameObject player;
    public float speed = 1;
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
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (health.currentHealth <= 0)
        {
            gameObject.GetComponent<Character>().Die();
        }
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
        
    }
}
