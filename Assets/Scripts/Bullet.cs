
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private Transform target;
    public float damageAmount;

    public float speed = 70f;

    public void Seek (Transform _target, float damage)
    {
        target = _target;
        damageAmount = damage;
    }
    
    
    void Update()
    {
        if (target == null) 
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame= speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        Destroy(gameObject);
        EnemyGeneral enemy = target.GetComponent<EnemyGeneral>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
        }

    }


}
