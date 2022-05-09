using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [Header("Firetrap Sound")]
    [SerializeField] private AudioClip firetrapSound;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Health playerHealth;
    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        StartCoroutine(AutomatedActivation());
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void activate()
    {
        if (!triggered)
        {
            StartCoroutine(ActivateFiretrap());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.tag == "Player")
            if(!triggered)
            {
                StartCoroutine(ActivateFiretrap());
            }*/
        if(collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerHealth = null;
    }

    private IEnumerator AutomatedActivation()
    {
        yield return new WaitForSeconds(activationDelay * Time.deltaTime);
        activate();
    }

    private IEnumerator ActivateFiretrap()
    {
        //Trigger
        triggered = true;
        //spriteRenderer.color = Color.red;
        //Activate
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
       // spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("activated", true);
        //Deactivate
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
