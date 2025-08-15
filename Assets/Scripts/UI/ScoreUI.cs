using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text helpedText;
    private void Start()
    {
        if (GameManager.Instance != null)
        {
            //calculate the percentage of NPCs help (solved puzzles)
            helpedText.text = "NPCs Helped: " + GameManager.Instance.NPCsHelped;
        }
    }
}
