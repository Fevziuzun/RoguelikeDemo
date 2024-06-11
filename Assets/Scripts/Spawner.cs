using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    
    public GameObject[] enemies;
    
    void Start()
    {
        
        InvokeRepeating("spawnEnemy", 1f, 5f);
    }

    public void spawnEnemy()
    {
        
        Instantiate(enemies[Random.Range(0, enemies.Length)],transform.position + new Vector3(Random.Range(0, 5), Random.Range(0, 5), Random.Range(0, 5)), Quaternion.identity);
            
    }
}
