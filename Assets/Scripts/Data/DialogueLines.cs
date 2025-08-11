using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLines", menuName = "Dialogue/Lines")]
public class DialogueLines : ScriptableObject
{
    [TextArea]
    public string[] Lines;
}
