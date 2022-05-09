using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header ("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private bool stopped = false;
    [Header("Impact Sound")]
    [SerializeField] private AudioClip impactSound;
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];
    private void Update()
    {
        if(attacking)
        transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //Right
        directions[1] = -transform.right * range; //Left
        directions[2] = transform.up * range; //Up
        directions[3] = -transform.up * range; //Down

    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
        stopped = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") return;
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        //Stop once it hits somehing
        if(collision.tag == "Ground")
        Stop();
       
    }

    private void OnEnable()
    {
        Stop();
    }
}
