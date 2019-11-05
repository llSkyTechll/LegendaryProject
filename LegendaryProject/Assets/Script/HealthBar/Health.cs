﻿using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    public int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private void OnEnable(){
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

        if (currentHealth <= 0)
        {
            GetComponent<Character>().Die();
        }
    }
    public void GainHealth(int heal)
    {
        if (currentHealth < maxHealth && currentHealth>0)
        {
            currentHealth += heal;

            float currentHealthPct = (float)currentHealth / (float)maxHealth;
            OnHealthPctChanged(currentHealthPct);
        }
    }
}
