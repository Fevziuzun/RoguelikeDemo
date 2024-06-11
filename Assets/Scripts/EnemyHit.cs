using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private Transform target;
    public float damageAmount = 10f;
    public float damageInterval = 2f;

    void HitTarget()
    {
        if (target != null)
        {
            PlayerGeneral player = target.GetComponent<PlayerGeneral>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            InvokeRepeating("HitTarget", 0f, damageInterval);

        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke("HitTarget");
            target = null;
        }
    }


}
