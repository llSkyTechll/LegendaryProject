using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public float speed = 1;
    public Interactable focus;
    CharacterController controller;
    MouvementCamera sceneCamera;
    private CameraFocus cameraFocus;
    Armor playerArmor;
    public int damageReduction = 0;
    private Animator animator;
    private int healthRegen = 1;
    private float healthRegenCD = 0f;
    public AudioClip deathSound;

    // Use this for initialization
    void Start()
    {
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
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            gameObject.GetComponent<Character>().Die();
        }
        Footsteps();
        HealthRegen();
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

    public void HealthRegen()
    {
        if (healthRegenCD <= 0f)
        {
            health.GainHealth(healthRegen);
            healthRegenCD = 2f;
        }
        else
        {
            healthRegenCD -= Time.deltaTime;
        }
    }

    public override void Die()
    {
        if (!isDead)
        {
            animator.SetTrigger("Dead");
            PlaySound(deathSound);
            GetComponentInChildren<ParticleSystem>().Play();
            isDead = true;
        }
        if (!GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public bool GetIsDead()
    {
        return isDead;
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
        if (controller.velocity.magnitude > 0.5f && controller.velocity.magnitude < 5f && GetComponent<movementplayer>().isGrounded)
        {
            PlayRepeatingSound(step);
        }
        else if (controller.velocity.magnitude > 5f && GetComponent<movementplayer>().isGrounded)
        {
            PlayRepeatingSound(step,0.5f);
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

