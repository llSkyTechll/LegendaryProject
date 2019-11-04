using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : EnemyAI
{
    protected System.Random randomMove = new System.Random();
    private Transform startPoint;
    private Transform endPoint;

    private LineRenderer laserLine;
    private GameObject laser;

    //string[] Attacks = new string[] { "AttackTail", "JumpStationary", "AttackBite" };
    //string[] Deaths = new string[] { "DeathHit", "DeathKnockback" };
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
    [System.Obsolete]
    protected override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PositionManager.instance.player.transform;
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        player = PositionManager.instance.player;
        soundplayer = GetComponent<AudioSource>();
        playerArmor = player.GetComponentInChildren<Armor>();
        if (playerArmor != null)
        {
            damageReduction = playerArmor.damageBlocked;
        }
        playerHealth = player.GetComponent<Health>();
        animationRunName = GetAnimationRunName();
        animationAttackName = GetAnimationAttackName();
        animationDeadName = GetAnimationDeadName();
        animationIdleName = GetAnimationIdleName();
        lookRadius = SetlookRadius();
        startPoint = PositionManager.instance.LaserStart.transform;
        endPoint = PositionManager.instance.LaserEnd.transform;
        laser = GameObject.FindGameObjectWithTag("Laser");
        laserLine = laser.GetComponent<LineRenderer>();
        laserLine.SetWidth(0.2f, 0.2f);
        laser.SetActive(false);
    }


    protected override void OnUpdate()
    {
        Footsteps();
        attackCooldown -= Time.deltaTime;
        TimeBeforeAction -= Time.deltaTime;
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
                int DistanceAttack = randomMove.Next(0, 2);
                if (TimeBeforeAction <= 0)
                {
                    ResetActionPossible();
                    if (DistanceAttack == 0)
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

                        Objectif();
                    }
                }

                FaceTarget();
            }
            else if (distance <= agent.stoppingDistance)
            {
                int CloseAttack = randomMove.Next(0, 2);
                if (attackCooldown <= 0f)
                {
                    if (CloseAttack == 0)
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
            }
        }

    }

    void AttackTail()
    {
        print("Tail");
        animator.Play(animationAttackName);
        DealDamage();
        //player.GetComponent<Health>().TakeDamage(10);
        attackCooldown = 2f / attackSpeed;
    }

    void AttackLaser()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            //print("set");
            if (hit.collider)
            {
                laserLine.SetPosition(1, hit.point);
                //print("hit");
            }
        }
    }
    void Objectif()
    {
        laser.SetActive(true);
        print("Aim");
        Aim = true;
        endPoint.position = target.position;
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
        animator.Play(animationIdleName);
    }

    void AttackJump()
    {
        print("Jump");
        animator.Play(animationAttackName);
        DealDamage();
        attackCooldown = 2f / attackSpeed;
    }
    void Run()
    {
        print("run");
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
    }
    
}