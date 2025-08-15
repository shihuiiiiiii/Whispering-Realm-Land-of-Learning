using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalNPCs = 5;
    public int NPCsHelped = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keep even when scene change
        }
        else 
        { 
            Destroy(gameObject); //prevent duplicates
        }
    }
    public void NPCHelped()
    {
        NPCsHelped++;
    }
}
