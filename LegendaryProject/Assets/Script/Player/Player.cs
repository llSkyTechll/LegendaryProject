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
    private CameraFocus cameraFocus;
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
        focus = null;
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
        if (Input.GetButtonDown("Interact") )
        {
            if (Input.GetButtonDown("Interact") && focus != null)
            {
                sceneCamera.ResetFocus();
                focus = null;
            }
            else
            {
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
        cameraFocus = newFocus.GetComponentInChildren<CameraFocus>();
        focus = newFocus;
        if (cameraFocus!=null)
        {
            sceneCamera.ChangeFocus(cameraFocus.transform);
        }
        else
        {
            sceneCamera.ChangeFocus(newFocus.transform);
        }
        
    }
}
