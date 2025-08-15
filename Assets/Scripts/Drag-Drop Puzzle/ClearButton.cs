using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : MonoBehaviour
{
    public Transform wordArea;
    public Transform dropWordsArea;
    public void ClearDroppedWords()
    {
        //Array for children in dropWordsArea
        Transform[] children = new Transform[dropWordsArea.childCount];
        for (int i = 0; i < dropWordsArea.childCount; i++)
        {
            children[i] = dropWordsArea.GetChild(i);
        }

        foreach (Transform child in children)
        {
            child.SetParent(wordArea);
        }
    }
}
