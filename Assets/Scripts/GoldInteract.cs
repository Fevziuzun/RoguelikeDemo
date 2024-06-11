using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerGeneral>().currency += 5;
            Destroy(gameObject);
        }
    }
}
