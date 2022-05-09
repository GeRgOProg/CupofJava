using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintText : MonoBehaviour
{
    public static PrintText instance { get; private set; }

    public void Awake()
    {
        instance = this;
    }

    public void Print(string str)
    {
        GetComponent<TextMeshProUGUI>().text = str;
        
    }
}
