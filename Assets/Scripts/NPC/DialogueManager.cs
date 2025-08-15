using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager;
    public int dialogueNum = 0;
    void Awake()
    {
        if (dialogueManager == null) //check if there is already a instance of DialogueManager
        {
            dialogueManager = this; //remember this instance so can use it again later when scene change back
            DontDestroyOnLoad(gameObject); //keep this instance even when scene change
        }
        else
        {
            Destroy(gameObject); //destroy the new instance if theres already one there
        }
    }
}
