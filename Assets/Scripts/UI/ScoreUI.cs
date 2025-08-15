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
            helpedText.text = "NPCs Helped: " + ((float)GameManager.Instance.NPCsHelped/ GameManager.Instance.totalNPCs * 100f).ToString("F0") + "%"; //set percentage to show no decimal place using F0
        }
    }
}
