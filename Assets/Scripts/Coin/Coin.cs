using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    
    private float currentCoins;
    [SerializeField] private AudioClip pickupSound;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        currentCoins = player.GetComponent<Health>().getCoins();    
    }

    private void Update()
    {
        currentCoins = player.GetComponent<Health>().getCoins();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickupSound);
            player.GetComponent<Health>().foundCoin();
            PrintText.instance.Print("Coins: " + (currentCoins + 1));
            gameObject.SetActive(false);
        }
    }


}
