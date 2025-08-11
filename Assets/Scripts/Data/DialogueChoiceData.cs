using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //to show class in inspector
public class DialogueChoice
{
    [TextArea]
    public string PlayerChoice; //option that player choose to say

    [TextArea]
    public string NpcResponse; //what the npc reply for that option
}

[CreateAssetMenu (fileName = "DialogueChoice", menuName = "Dialogue/Choice")]
public class DialogueChoiceData : ScriptableObject
{
    public DialogueChoice[] Choices;
}