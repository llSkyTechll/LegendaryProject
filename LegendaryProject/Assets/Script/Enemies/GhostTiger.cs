using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostTiger : EnemyAI {
    public float speed = 1;
    protected Rigidbody rbd;

    public AudioClip attackSound;


    protected override string GetAnimationRunName()
    {
        return "run";
    }

  protected override string GetAnimationAttackName()
    {
        return "Skill2";
    }

    protected override string GetAnimationDeadName()
    {
        return "Dead";
    }

    protected override string GetAnimationIdleName()
    {
        return "Idle";
    }
    protected override int SetMaxLife()
    {
        return 100;
    }

    // Use this for initialization
 
    protected override void OnStart()
    {       
        rbd = GetComponent<Rigidbody>();
        SetLife(100);
        attack = attackSound;

        animationRunName = GetAnimationRunName();
        animationAttackName = GetAnimationAttackName();
        animationDeadName = GetAnimationDeadName();
        animationIdleName = GetAnimationIdleName();
        lookRadius = SetlookRadius();
        MaxLife = SetMaxLife();
    }

    protected override void OnUpdate()
    {
        Footsteps();
        attackCooldown -= Time.deltaTime;
        if (health.currentHealth <= 0)
        {
            animator.Play(animationDeadName);
            gameObject.GetComponent<Character>().Die();
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);
          
            if (distance <= lookRadius && distance > agent.stoppingDistance)
            {
                agent.SetDestination(target.position);
                if (attackCooldown <= 0f && agent.velocity.magnitude != 0)
                {
                    animator.Play(animationRunName);

                }

                FaceTarget();
            }
            else if (distance <= agent.stoppingDistance)
            {
                if (attackCooldown <= 0f)
                {
                    animator.Play(animationAttackName);
                    DealDamage(10);
                    PlaySound(attackSound);
                    attackCooldown = 2f / attackSpeed;
                }
                FaceTarget();
            }
            else if (distance > lookRadius && (agent.velocity.magnitude) == 0)
            {
                animator.Play(animationIdleName);
                PlayRepeatingSound(idleSound);
            }
        }
    }

    protected override float SetlookRadius()
    {
        return 10f;
    }

    protected override string GetName()
    {
        return "GhostTiger";
    }
}