using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(AudioSource))]
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
    protected AudioClip attack;
    protected AudioClip deathSound;

    protected int damageReduction = 0;
    protected string animationRunName = "Run";
    protected string animationAttackName = "Skill2";
    protected string animationDeadName = "Dead";
    protected string animationIdleName = "Idle";



    void Start()
    {
        player = PositionManager.instance.player;
        soundplayer = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        target = PositionManager.instance.player.transform;
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
        if(!isDead)
        {
            isDead = true;
            animator.SetTrigger("Death");
            PlaySound(attack,-0.6f);
            Destroy(gameObject, 10);
        }
    }

    protected void DealDamage(int damage)
    {
        player.GetComponent<Player>().OnDamage(damage);
    }

    public override void OnDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    public override void Footsteps()
    {
        if (agent.velocity.magnitude > 2 && soundplayer.isPlaying == false)
        {
            PlayRepeatingSound(step);
        }
    }

    protected void PlayRepeatingSound(AudioClip clip, float pitchOffset = 0, float volumeOffset = 0)
    {
        if (!soundplayer.isPlaying)
        {
            soundplayer.clip = clip;
            soundplayer.pitch = UnityEngine.Random.Range(0.8f + pitchOffset, 1.2f + pitchOffset);
            soundplayer.volume = UnityEngine.Random.Range(0.8f + volumeOffset, 1.1f + volumeOffset);
            soundplayer.Play();
        }
    }

    protected void PlaySound(AudioClip clip, float pitchOffset = 0, float volumeOffset = 0)
    {
        soundplayer.pitch = UnityEngine.Random.Range(0.8f + pitchOffset, 1.2f + pitchOffset);
        soundplayer.volume = UnityEngine.Random.Range(0.8f + volumeOffset, 1.1f + volumeOffset);
        soundplayer.PlayOneShot(clip);
    }
}
