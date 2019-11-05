using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Boss : EnemyAI
{
    protected System.Random randomMove = new System.Random();
    private Transform startPoint;
    private Transform endPoint;

    private LineRenderer laserLine;
    private GameObject laser;
    //Rigidbody rdbPlayer;
    //BoxCollider bossCollider;
    ParticleSystem jumpParticle;

    public AudioClip attackSound;
    public AudioClip charge;
    public AudioClip laserSound;
    public AudioClip death;

    protected override string GetAnimationAttackName()
    {
        return "AttackTail";
    }
    protected override string GetAnimationDeadName()
    {
        return "DeathHit";
    }
    protected override string GetAnimationIdleName()
    {
        return "Idle";
    }
    protected override string GetAnimationRunName()
    {
        return "Run";
    }
    protected override float SetlookRadius()
    {
        return 50f;
    }
    bool Aim = false;
    bool Moving = false;
    float TimeBeforeAction = 5f;
    float TimeBeforeShoot = 4f;

    protected override void OnStart()
    {
        SetLife(300);


        // bossCollider = GetComponentInChildren<BoxCollider>();
        jumpParticle = GetComponentInChildren<ParticleSystem>();
        //rdbPlayer = player.GetComponent<Rigidbody>();
        animationRunName = GetAnimationRunName();
        animationAttackName = GetAnimationAttackName();
        animationDeadName = GetAnimationDeadName();
        animationIdleName = GetAnimationIdleName();
        lookRadius = SetlookRadius();

        startPoint = PositionManager.instance.LaserStart.transform;
        endPoint = PositionManager.instance.LaserEnd.transform;
        laser = GameObject.FindGameObjectWithTag("Laser");
        laserLine = laser.GetComponent<LineRenderer>();
        laser.SetActive(false);

        attack = attackSound;
    }


    protected override void OnUpdate()
    {

        Footsteps();
        attackCooldown -= Time.deltaTime;
        TimeBeforeAction -= Time.deltaTime;
        if (health.currentHealth <= 0)
        {
            if (!isDead)
            {
                //animator.Play(animationDeadName);
                gameObject.GetComponent<Character>().Die();
            }
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius && distance > agent.stoppingDistance)
            {
                int RandomDistanceAttack = randomMove.Next(0, 4);
                if (TimeBeforeAction <= 0)
                {

                    ResetActionPossible();
                    if (RandomDistanceAttack <= 2)
                    {
                        Run();
                        TimeBeforeAction = 5f;
                    }
                    else
                    {
                        animationAttackName = "AttackBite";
                        Objectif();
                        TimeBeforeAction = 5f;
                    }
                }
                else
                {
                    if (Moving)
                    {
                        Run();
                    }
                    else if (Aim)
                    {
                        if (TimeBeforeShoot <= 0)
                        {
                            AttackLaser();
                        }
                        else
                        {
                            Objectif();
                        }

                    }
                }

                FaceTarget();
            }
            else if (distance <= agent.stoppingDistance)
            {
                int RandomCloseAttack = randomMove.Next(0, 2);
                laser.SetActive(false);
                if (attackCooldown <= 0f)
                {
                    if (RandomCloseAttack == 0)
                    {
                        animationAttackName = "AttackTail";
                        AttackTail();
                    }
                    else
                    {
                        animationAttackName = "JumpStationary";
                        AttackJump();
                    }
                }
                FaceTarget();
            }
            else if (distance > lookRadius && (agent.velocity.magnitude / agent.speed) == 0)
            {
                animator.Play(animationIdleName);
                laser.SetActive(false);
            }
        }

    }

    void OnDestroy()
    {
        if (isDead)
        {
            SceneManager.LoadScene("Win");
        }
    }

    void AttackTail()
    {
        animator.Play(animationAttackName);
        DealDamage(10);
        attackCooldown = 2f / attackSpeed;
    }
    void Objectif()
    {
        agent.SetDestination(transform.position);
        TimeBeforeShoot -= Time.deltaTime;
        laser.SetActive(true);
        Aim = true;
        endPoint.position = target.position;
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
        animator.Play(animationIdleName);
        PlayRepeatingSound(charge,0,-0.3f);
    }
    void AttackLaser()
    {
        TimeBeforeShoot = 4f;
        RaycastHit hit;
        animator.Play(animationAttackName);
        PlaySound(laserSound);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                laserLine.SetPosition(1, hit.point);
                DealDamage(5);
            }
        }

    }

    void AttackJump()
    {
        jumpParticle.Play();
        animator.Play(animationAttackName);
        DealDamage(15);
        attackCooldown = 2f / attackSpeed;
        PlaySound(step,-0.5f);
    }
    void Run()
    {
        agent.SetDestination(target.position);
        if (attackCooldown <= 0f && agent.velocity.magnitude / agent.speed != 0)
        {
            animator.Play(animationRunName);
        }
        Moving = true;
    }

    void ResetActionPossible()
    {
        Moving = false;
        Aim = false;
        laser.SetActive(false);
    }


}