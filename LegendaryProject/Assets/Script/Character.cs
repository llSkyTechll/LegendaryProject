using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Character : MonoBehaviour, Damageable
{
    protected Health health;
    public int lifeTotal = 1;

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
}