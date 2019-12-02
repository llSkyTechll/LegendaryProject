using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyAI {
    protected Rigidbody rbd;
    public float speed = 1;
    public AudioClip attackSound;
    protected override string GetAnimationAttackName()
    {
        return "attack";
    }

    protected override string GetAnimationDeadName()
    {
        return "dead";
    }

    protected override string GetAnimationIdleName()
    {
        return "idle";
    }

    protected override string GetAnimationRunName()
    {
        return "run";
    }

    protected override void OnStart()
    {
        rbd = GetComponent<Rigidbody>();
        //audioSource = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();
        SetLife(30);
        attack = attackSound;
    }

    protected override void OnUpdate()
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
                    animator.Play(animationRunName);

                }

                FaceTarget();
            }
            else if (distance <= agent.stoppingDistance)
            {
                if (attackCooldown <= 0f)
                {
                    animator.Play("Skill2");
                    DealDamage(10);
                    PlaySound(attackSound);
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

    protected override float SetlookRadius()
    {
        return 20f;
    }
}
