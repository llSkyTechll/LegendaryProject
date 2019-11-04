using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : Character
{

    public float lookRadius = 25f;

    protected Transform target;
    protected NavMeshAgent agent;

    protected float attackSpeed = 1f;
    protected float attackCooldown = 0f;

    protected GameObject player;
    protected Animator animator;
    protected Armor playerArmor;
    protected Health playerHealth;
    protected AudioClip attack;

    protected int damageReduction = 0;
    protected string animationRunName="Run";
    // Use this for initialization


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        player = PlayerManager.instance.player;
        soundplayer = GetComponent<AudioSource>();
        playerArmor = player.GetComponentInChildren<Armor>();
        if (playerArmor != null)
        {
            damageReduction = playerArmor.damageBlocked;
        }
        playerHealth = player.GetComponent<Health>();
        animationRunName = GetAnimationRunName();
    }

    // Update is called once per frame
    protected void OnUpdate()
    {
        print(agent);
        Footsteps();
        attackCooldown -= Time.deltaTime;
        if (health.currentHealth <= 0)
        {
            animator.Play("Dead");
            gameObject.GetComponent<Character>().Die();
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius && distance > agent.stoppingDistance)
            {
                agent.SetDestination(target.position);
                if (attackCooldown <= 0f && agent.velocity.magnitude / agent.speed != 0)
                {
                    animator.Play(animationRunName);

                }

                FaceTarget();
            }
            else if (distance <= agent.stoppingDistance)
            {
                if (attackCooldown <= 0f)
                {
                    animator.Play("Skill2");
                    DealDamage();
                    //player.GetComponent<Health>().TakeDamage(10);
                    attackCooldown = 2f / attackSpeed;
                }
                FaceTarget();
            }
            else if (distance > lookRadius && (agent.velocity.magnitude / agent.speed) == 0)
            {
                animator.Play("Idle");
            }
        }
    }
    protected abstract string GetAnimationRunName();
    
    void Update()
    {
        OnUpdate();
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public override void Die()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 10);
    }

    void DealDamage()
    {
        int damage = 10;
        damage = damage - damageReduction;
        playerHealth.TakeDamage(damage);
        AttackSound();
    }

    void AttackSound()
    {
        soundplayer.clip = attack;
        soundplayer.pitch = Random.Range(0.8f, 1.2f);
        soundplayer.volume = Random.Range(0.8f, 1.1f);
        soundplayer.Play();
    }

    public override void OnDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public override void Footsteps()
    {
        //if (agent.velocity.magnitude > 2f && soundplayer.isPlaying == false)
        if (agent.velocity.magnitude > 2f)
        {
            if (soundplayer.isPlaying == false)
            {
                soundplayer.clip = step;
                soundplayer.pitch = Random.Range(0.8f, 1);
                soundplayer.volume = Random.Range(0.8f, 1.1f);
                soundplayer.Play();
            }
        }
    }
}
