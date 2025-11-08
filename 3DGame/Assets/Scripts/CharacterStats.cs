using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float power = 10f;
    int killScore = 200;
    public float currentHealth { get; private set; }
    
    void Start()
    {
        currentHealth = (int)maxHealth;
        
    }

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;
        Debug.Log("Current Health: " + currentHealth + " / " + maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }
    
    public void Die()
    {
        if (transform.CompareTag("Player"))
        { 
            
        }else if (transform.CompareTag("Enemy"))
        {
            LevelManager.instance.score += killScore;
            Destroy(gameObject);
        }
    }
}
