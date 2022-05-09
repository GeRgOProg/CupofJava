using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject player;
    private bool gameOver = false;
    public void Update()
    {
        if(gameOver)
        if (Input.anyKeyDown)
        {

            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.tag == "Player")
             {
            gameOver = true;
            player.SetActive(false);
                gameOverScreen.SetActive(true);
           
             }
    }
}
