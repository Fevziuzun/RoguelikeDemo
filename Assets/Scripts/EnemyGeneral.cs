using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGeneral : MonoBehaviour
{
    public float lookRadius = 10f;
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject coinprefab;
    public float jumpForce = 5f; 
    public float randomRotationMax = 45f;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        currentHealth = maxHealth;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    FaceTarget();
                }
            }
        }
        
    }

    void FaceTarget()
    {
        Vector3 direction= (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f) 
        {
            Die();
        }
    }

    void Die() 
    {
        Destroy(gameObject);
        GameObject gold = Instantiate(coinprefab, transform.position, Quaternion.identity);
        Rigidbody goldRb = gold.GetComponent<Rigidbody>();
        if (goldRb != null)
        {
            Vector3 jumpDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
            goldRb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
        gold.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
