using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : GhostTiger {

    public float lookRadius = 25f;

    Transform target;
    NavMeshAgent agent;

    private float attackSpeed = 1f;
    private float attackCooldown = 0f;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        player = PlayerManager.instance.player;
    }
	
	// Update is called once per frame
	void Update () {
        attackCooldown -= Time.deltaTime;
        if (health.currentHealth <= 0)
        {
            animator.Play("Dead");
            gameObject.GetComponent<Character>().Die();
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius && distance >= agent.stoppingDistance)
            {
                agent.SetDestination(target.position);
                animator.Play("Run");
                if (distance <= agent.stoppingDistance)
                {
                    if (attackCooldown <= 0f)
                    {
                        animator.Play("Skill2");
                        player.GetComponent<Health>().TakeDamage(10);
                        attackCooldown = 2f / attackSpeed;
                    }
                    FaceTarget();
                }
            }
            else
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
}
