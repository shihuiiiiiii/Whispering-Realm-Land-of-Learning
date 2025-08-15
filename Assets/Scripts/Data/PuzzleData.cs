using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="PuzzleData", menuName= "Puzzles/SentenceData")]
public class PuzzleData : ScriptableObject
{
    public List<string> Sentence;
}
