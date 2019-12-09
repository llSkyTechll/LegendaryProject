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
    ParticleSystem jumpParticle;

    public AudioClip attackSound;
    public AudioClip sweepSound;
    public AudioClip charge;
    public AudioClip laserSound;
    AudioClip death;

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
    protected override int SetMaxLife()
    {
        return 300;
    }
    bool Aim = false;
    bool Moving = false;
    float TimeBeforeAction = 5f;
    float TimeBeforeShoot = 4f;

    protected override void OnStart()
    {
        MaxLife = SetMaxLife();


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
                animator.Play(animationDeadName);
                Die();
            }
        }
        else
        {
            BeAlive();
        }

    }

    void BeAlive()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius && distance > agent.stoppingDistance)
        {
            DoFarAction();
        }
        else if (distance <= agent.stoppingDistance)
        {
            DoNearAction();
        }
        else if (distance > lookRadius && (agent.velocity.magnitude / agent.speed) == 0)
        {
            animator.Play(animationIdleName);
            PlayRepeatingSound(idleSound, -0.2f);
            laser.SetActive(false);
        }
    }

    void DoFarAction()
    {
        if (TimeBeforeAction <= 0)
        {
            ChangeFarAction();
        }
        else
        {
            KeepDoingFarAction();
        }

        FaceTarget();
    }

    void KeepDoingFarAction()
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

    void ChangeFarAction()
    {
        int RandomDistanceAttack = randomMove.Next(0, 4);
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

    void DoNearAction()
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
        PlaySound(sweepSound,-0.5f);
        DealDamage(10);
        attackCooldown = 2f / attackSpeed;
    }
    void Objectif()
    {
        laserLine.enabled = true;
        agent.SetDestination(transform.position);
        TimeBeforeShoot -= Time.deltaTime;
        laser.SetActive(true);
        Aim = true;
        endPoint.position = target.position;
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
        animator.Play(animationIdleName);
        PlayRepeatingSound(charge,-0.6f,-0.69f);
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
        Aim = false;
        laserLine.enabled = false;
    }

    void AttackJump()
    {
        jumpParticle.Play();
        animator.Play(animationAttackName);
        DealDamage(15);
        attackCooldown = 2f / attackSpeed;
        PlaySound(step,-0.5f,2,1.2f);
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
        TimeBeforeShoot = 4f;

    }

    protected override string GetName()
    {
        return "MrBaleine";
    }

}