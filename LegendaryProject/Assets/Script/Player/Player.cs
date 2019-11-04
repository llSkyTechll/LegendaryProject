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
    Armor playerArmor;
    public int damageReduction = 0;
    //Rigidbody rbd;
    //AudioSource audioSource;
    private Animator animator;
    private bool isDead;
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
        soundplayer = GetComponent<AudioSource>();
        playerArmor = GetComponentInChildren<Armor>();
        if (playerArmor != null)
        {
            damageReduction = playerArmor.damageBlocked;
        }
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            gameObject.GetComponent<Character>().Die();
        }
        Footsteps();
        if (Input.GetButtonDown("Interact"))
        {
            if (Input.GetButtonDown("Interact") && focus != null)
            {
                sceneCamera.ResetFocus();
                focus = null;
                SceneManager.LoadScene("Boss Room");
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
        if (!isDead)
        {
            animator.SetTrigger("Dead");
            GetComponentInChildren<ParticleSystem>().Play();
            isDead = true;
        }
        if (!GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public override void OnDamage(int damage)
    {
        damage -= damageReduction;
        if (damage > 0)
        {
            health.TakeDamage(damage);
        }

    }

    public override void Footsteps()
    {
        if (controller.velocity.magnitude > 0.5f && controller.velocity.magnitude < 5f && soundplayer.isPlaying == false && GetComponent<movementplayer>().isGrounded)
        {
            soundplayer.clip = step;
            soundplayer.pitch = Random.Range(0.8f, 1);
            soundplayer.volume = Random.Range(0.8f, 1.1f);
            soundplayer.Play();
        }
        else if (soundplayer.isPlaying == false && controller.velocity.magnitude > 5f && GetComponent<movementplayer>().isGrounded)
        {
            soundplayer.clip = step;
            soundplayer.pitch = Random.Range(1.3f, 1.5f);
            soundplayer.volume = Random.Range(0.8f, 1.1f);
            soundplayer.Play();
        }
    }
    void SetFocus(Interactable newFocus)
    {
        cameraFocus = newFocus.GetComponentInChildren<CameraFocus>();
        focus = newFocus;
        if (cameraFocus != null)
        {
            sceneCamera.ChangeFocus(cameraFocus.transform);
        }
        else
        {
            sceneCamera.ChangeFocus(newFocus.transform);
        }

    }

}

