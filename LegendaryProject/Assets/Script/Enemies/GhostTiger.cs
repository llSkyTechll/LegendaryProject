using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostTiger : EnemyAI {
    public float speed = 1;
    protected Rigidbody rbd;
    AudioSource audioSource;

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

    //public AudioMusic audioMusic;

    // Use this for initialization
 
    protected override void OnStart()
    {       
        rbd = GetComponent<Rigidbody>();
        //audioSource = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();
        SetLife(100);
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
        return 25f;
    }



    // Update is called once per frame


    //private void FollowPlayer()
    //{
    //    animator.Play("Run");
    //    rbd.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
    //}
}