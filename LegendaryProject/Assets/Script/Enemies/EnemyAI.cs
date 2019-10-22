using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Character
{

    public float lookRadius = 25f;

    Transform target;
    NavMeshAgent agent;

    private float attackSpeed = 1f;
    private float attackCooldown = 0f;

    protected GameObject player;
    protected Animator animator;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        player = PlayerManager.instance.player;
        stepplayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
                   animator.Play("Run");
                }
                
                FaceTarget();
            }
            else if (distance <= agent.stoppingDistance)
            {
                if (attackCooldown <= 0f)
                {
                    animator.Play("Skill2");
                    player.GetComponent<Health>().TakeDamage(10);
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

    public override void OnDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public override void Footsteps()
    {
        if (agent.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
        {
            stepplayer.pitch = Random.Range(0.8f, 1);
            stepplayer.volume = Random.Range(0.8f, 1.1f);
            stepplayer.Play();
        }
    }
}
