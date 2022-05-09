using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private GameObject[] activeEnemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        activeEnemies = new GameObject[enemies.Length];
        initialPosition = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            initialPosition[i] = enemies[i].transform.position;

            if (enemies[i].activeInHierarchy)
                activeEnemies[i] = enemies[i];
        }
    }
    
    public void ActivateRoom(bool _status)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (activeEnemies[i] != null)
            {
                
                    activeEnemies[i].SetActive(_status);
                    activeEnemies[i].transform.position = initialPosition[i];
                
            }
        }
    }
}
