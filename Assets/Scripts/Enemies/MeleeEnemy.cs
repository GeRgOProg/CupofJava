using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [Header("Collider Parameters")]
    [SerializeField] private float distance;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    [Header("Attack Sound")]
   [SerializeField] private AudioClip attackSound;
  //References
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //Player in sight?
        if(PlayerInSight())
        if(cooldownTimer >= attackCooldown)
        {
                //Attack
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private bool PlayerInSight()
    {


        if (GetComponent<Health>().currentHealth == 0) return false;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center+transform.right * range * transform.localScale.x * distance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            
          
            playerHealth = hit.transform.GetComponent<Health>();
            if (playerHealth.currentHealth == 0)
                return false;
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * distance , new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


    private void DamagePlayer()
    {
        if (GetComponent<Health>().currentHealth == 0) return;
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
