using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour
{
    public void Play(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
