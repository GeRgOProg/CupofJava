using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float totalCoins;
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private bool invulnerable;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    [SerializeField]  private SpriteRenderer spriteRenderer;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    [Header("Hurt Sound")]
    [SerializeField] private AudioClip hurtSound;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        totalCoins = 0;
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void TakeDamage(float _damage)
    {
        if (currentHealth == 0) return;
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0)
        {
            //Hurts
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
           
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if(gameObject.tag != "Player")
            anim.SetTrigger("die");
            //Dies
            if (!dead)
            {
                
                ////Player
                //if(GetComponent<PlayerMovement>() != null)
                //GetComponent<PlayerMovement>().enabled = false;
                //if (GetComponent<PlayerAttack>() != null)
                //    GetComponent<PlayerAttack>().enabled = false;

                ////Enemy
                //if (GetComponentInParent<EnemyPatrol>() != null)
                //    GetComponentInParent<EnemyPatrol>().enabled = false;
                //if (GetComponent<MeleeEnemy>() != null)
                //    GetComponent<MeleeEnemy>().enabled = false;

                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8,9,true);
        //duration waiting
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.7f);
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / numberOfFlashes);
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public float getCoins()
    {
        return totalCoins;
    }

    public void foundCoin()
    {
        totalCoins += 1;
    }
}
