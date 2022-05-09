using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private GameObject deathScreen;

   

    private void Update()
    {
        if (player.GetComponent<Health>().currentHealth == 0)
        {
           
            deathScreen.SetActive(true);
            if (Input.anyKeyDown)
            {
                deathScreen.SetActive(false);
               
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
    }

}
