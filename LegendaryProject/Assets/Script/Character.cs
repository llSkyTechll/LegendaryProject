using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public abstract class Character : MonoBehaviour, Damageable
{
    protected Health health;
    protected int lifeTotal = 1;
    protected AudioSource soundplayer;
    public AudioClip step;

    public void TakeDamage(int damage)
    {
        OnDamage(damage);
    }

    public abstract void OnDamage(int damage);

    public abstract void Die();

    protected void SetLife(int newLifeTotal)
    {
        lifeTotal = newLifeTotal;
    }

    public abstract void Footsteps();
}