using System;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

    private Animator animator; 

    private void OnEnable(){
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void ModifiyHealth(int amount){
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(gameObject,10);
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
            ModifiyHealth(-10);
    }

    public void TakeDamage(int damage)
    {
        ModifiyHealth(-damage);
    }
}
