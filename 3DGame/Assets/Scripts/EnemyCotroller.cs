using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCotroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    NavMeshAgent agent;
    public float attackDistance = 1.0f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance > attackDistance)
        {
             agent.SetDestination(player.position);
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player Detected");
            Destroy(gameObject);
        }
    }
    
    
}
