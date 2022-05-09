using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject coinPouch;

    private void Update()
    {
        if (boss.GetComponent<Health>().currentHealth == 0)
        {
            coinPouch.SetActive(true);
            portal.SetActive(true);
        }
    }
}
