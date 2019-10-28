using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    //GameObject player;
    public float speed = 1;
    public Interactable focus;
    CharacterController controller;
    MouvementCamera sceneCamera;
    //Rigidbody rbd;
    //AudioSource audioSource;
    //private Animator animator;
    // Use this for initialization
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //rbd = GetComponent<Rigidbody>();
        //audioSource = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();
        sceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouvementCamera>();
        controller = GetComponent<CharacterController>();
        SetLife(100);
        health = GetComponent<Health>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            gameObject.GetComponent<Character>().Die();
        }
        if (Input.GetAxis("Interact") != 0)
        {
            Debug.DrawRay(transform.position, transform.forward * 2f, Color.green);
            
            Collider[] ThingsInRange = Physics.OverlapSphere(transform.position, 2f, 9);
            foreach (Collider Thing in ThingsInRange)
            {
                Interactable interactable = Thing.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
        if (Input.GetAxis("Cancel") != 0)
        {
            sceneCamera.ResetFocus();
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

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        sceneCamera.ChangeFocus(focus.transform);
    }
}
