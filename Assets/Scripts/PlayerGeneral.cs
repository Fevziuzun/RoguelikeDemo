using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    public Transform container;
    private Transform target;
    [Header("I love my wife")]
    [Header("Attributes")]
    public float currency = 0;
    public float maxHealth = 100f;
    public float currentHealth;
    public float range = 10f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float fireDamage = 10f;
    


    [Header("Unity Setup Field")]
    public string enemyTag = "Enemy";
    public Transform characterRotate;
    public float turnspeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public ParticleSystem damageEffect;






    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(container.rotation, lookRotation, Time.deltaTime * turnspeed).eulerAngles;
            container.rotation = Quaternion.Euler(0f, rotation.y, 0f);


            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float forwardInput = Input.GetAxis("Vertical");

            float lookRotation = Mathf.Atan2(horizontalInput,forwardInput)*Mathf.Rad2Deg;
            Vector3 rotation = Quaternion.Lerp(container.rotation, Quaternion.Euler(lookRotation * Vector3.up), Time.deltaTime * turnspeed).eulerAngles;
            container.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
          
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        damageEffect.Play();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent <Bullet>();

        if (bullet != null)
        {
            
            bullet.Seek(target, fireDamage);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy=Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance )
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
