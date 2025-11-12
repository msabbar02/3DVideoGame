using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float power = 10f;
    int killScore = 200;
    public float currentHealth { get; private set; }
    
    public Image healthBar;
    public TextMeshProUGUI healthText; 
    
    private Animator animator;
    private bool isDead = false; 

    void Start()
    {
        currentHealth = (int)maxHealth;
        animator = GetComponentInChildren<Animator>(); 
        
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        if (healthText != null)
        {
            float percent = (currentHealth / maxHealth) * 100f;
            healthText.text = Mathf.RoundToInt(percent).ToString() + "%"; 
        }
    }

    public void ChangeHealth(float amount)
    {
        if (isDead) return; 

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
        if (healthText != null)
        {
            float percent = (currentHealth / maxHealth) * 100f;
            healthText.text = Mathf.RoundToInt(percent).ToString() + "%";
        }
        
        if (currentHealth <= 0)
        {
            Die(); 
        }
    }
    
    public void Die()
    {
        
        if (isDead) return;
        isDead = true; 

        if (transform.CompareTag("Player"))
        { 
            Debug.Log("Player has died!");
            PlayerController controller = GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.enabled = false;
            }
            if (animator != null)
            {
               
                animator.SetTrigger("Die"); 
            }
            
            
            StartCoroutine(ReloadLevelAfterDelay(2.5f)); 
        }
        else if (transform.CompareTag("Enemy"))
        {
            LevelManager.instance.score += killScore;
            Destroy(gameObject); 
        }
    }
    IEnumerator ReloadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}