using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPuzzle : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainGame();
        }
    }
    void ReturnToMainGame()
    {
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.puzzleData = null;
        }
        SceneManager.LoadScene("MainGame");
    }
}
