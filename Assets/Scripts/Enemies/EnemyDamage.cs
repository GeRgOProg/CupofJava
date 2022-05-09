using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(GetComponent<Health>() == null)
            collision.GetComponent<Health>().TakeDamage(damage);
            else if(GetComponent<Health>().currentHealth != 0)
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
