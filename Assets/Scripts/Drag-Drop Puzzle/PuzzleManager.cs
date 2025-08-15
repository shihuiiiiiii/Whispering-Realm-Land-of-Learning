using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance; //allow other scripts to access
    public PuzzleData puzzleData; //store the data of the puzzle to bring over to puzzle scene

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keep puzzle manager across scenes
        }
        else
        {
            Destroy(gameObject); //make sures that there is only one puzzleManager
            return;
        }
        puzzleData = null; //reset puzzle data
    }
}
