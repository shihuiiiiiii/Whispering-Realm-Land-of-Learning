using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    public Transform wordArea;
    public Transform dropWordsArea;
    public void ClearDroppedWords()
    {
        foreach (Transform child in dropWordsArea)
        {
            child.SetParent(wordArea);
        }
    }
}
