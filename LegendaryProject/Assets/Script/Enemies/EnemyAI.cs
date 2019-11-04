using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : Character
{

    protected float lookRadius = 25f;

    protected Transform target;
    protected NavMeshAgent agent;

    protected float attackSpeed = 1f;
    protected float attackCooldown = 0f;

    protected GameObject player;
    protected Animator animator;
    protected Armor playerArmor;
    protected Health playerHealth;
    public AudioClip attack;

    protected int damageReduction = 0;
    protected string animationRunName = "Run";
    protected string animationAttackName = "Skill2";
    protected string animationDeadName = "Dead";
    protected string animationIdleName = "Idle";



    void Start()
    {
        OnStart();
    }
    protected abstract void OnStart();


    protected abstract string GetAnimationRunName();
    protected abstract string GetAnimationAttackName();
    protected abstract string GetAnimationDeadName();
    protected abstract string GetAnimationIdleName();
    protected abstract float SetlookRadius();


    void Update()
    {
        OnUpdate();

    }
    protected abstract void OnUpdate();
    
    protected void FaceTarget()
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

    protected void DealDamage()
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
        if (agent.velocity.magnitude > 2f && soundplayer.isPlaying == false)
        {
            soundplayer.clip = step;
            soundplayer.pitch = Random.Range(0.8f, 1);
            soundplayer.volume = Random.Range(0.8f, 1.1f);
            soundplayer.Play();

        }
    }
}
