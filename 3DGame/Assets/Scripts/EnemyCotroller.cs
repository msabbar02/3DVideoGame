using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyCotroller : MonoBehaviour
{
    // Start is called before the first frame update
    // public Transform player;
    CharacterStats stats;
    NavMeshAgent agent;
    Animator animator;
    public float attackDistance = 1.0f;
    
    bool canAttack = true;
    float attackCooldown = 2.0f;
    float lastAttackTime = 0.0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        float distance = Vector3.Distance(transform.position, LevelManager.instance.player.position);
        if (distance > attackDistance)
        {
             agent.SetDestination(LevelManager.instance.player.position);
             if (distance <= agent.stoppingDistance)
             {
                 if (canAttack)
                 { 
                     // Attack
                        animator.SetTrigger("Attack");
                        LevelManager.instance.PlaySound(LevelManager.instance.levelSounds[4], LevelManager.instance.player.position);
                        StartCoroutine(coolDown());
                 }
             }
        }
        
    }

    IEnumerator coolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stats.ChangeHealth(-other.GetComponentInParent<CharacterStats>().power);
        }
    }

    public void damagePlayer()
    {
        LevelManager.instance.player.GetComponent<CharacterStats>().ChangeHealth(-stats.power);
    }
    
    
}
