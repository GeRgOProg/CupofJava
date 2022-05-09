using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyProjectiles : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D collider;


    private void Awake()
    {

        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {

        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        collider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;

        transform.Translate(movementSpeed * -1, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
        collider.enabled = false;

        if (anim != null)
        {
            anim.SetTrigger("explode"); //Fireball
        }
        else
            gameObject.SetActive(false); //Arrow
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
