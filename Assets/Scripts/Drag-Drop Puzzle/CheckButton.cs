using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckButton : MonoBehaviour
{
    public ClearButton clearButton;

    public PuzzleData sentenceData;
    public Transform wordSpawnArea;
    public Transform dropWordsArea;
    public GameObject wordPrefab;

    //Audio
    public AudioSource audiosource;
    public AudioClip CorrectSfx;
    public AudioClip WrongSfx;

    private bool puzzleCompleted = false;
    public void Start()
    {
        SpawnWords();
    }
    public void SpawnWords()
    {
        foreach(Transform child in wordSpawnArea)
        {
            Destroy(child.gameObject); //to make sure the previous words from previous round of puzzle is cleared
        }

        //Shuffle the words
        List<string> shuffledWords = new List<string>(sentenceData.Sentence);

        for (int i = 0; i < shuffledWords.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledWords.Count);
            string word = shuffledWords[i];
            shuffledWords[i] = shuffledWords[randomIndex];
            shuffledWords[randomIndex] = word;
        }

        //Spawn the words
        foreach (string word in shuffledWords)
        {
            GameObject spawnWord = Instantiate(wordPrefab, wordSpawnArea);
            spawnWord.GetComponentInChildren<TMPro.TMP_Text>().text = word;
        }
    }
    public void CheckAnswer()
    {
        if (puzzleCompleted) return; //prevent double counting for the score
        Debug.Log("Checking Answer");
        List<string> droppedWords = new List<string>(); //list of the words dropped into the dropWordsArea

        //get the words in the word button
        foreach (Transform child in dropWordsArea)
        {
            TMP_Text droppedWordsText = child.GetComponentInChildren<TMP_Text>();
            if (droppedWordsText != null)
            {
                droppedWords.Add(droppedWordsText.text);
            }
        }

        //count the words in the droppedWords list to check whether the player have placed all the words
        if (droppedWords.Count != sentenceData.Sentence.Count)
        {
            Debug.Log("There are still missing words");
        }

        //Check whether the seentence have any word in wrong order by iterating through it
        for (int i = 0; i < sentenceData.Sentence.Count; i++)
        {
            if (droppedWords[i] != sentenceData.Sentence[i])
            {
                Debug.Log("Wrong answer");
                if (audiosource && WrongSfx)
                    audiosource.PlayOneShot(WrongSfx);

                if (clearButton != null)
                {
                    clearButton.ClearDroppedWords();
                }

                return;
            }
        }
        puzzleCompleted = true;
        Debug.Log("Correct Answer");
        if (audiosource && CorrectSfx)
            audiosource.PlayOneShot(CorrectSfx);

        //Tell GameManager that NPCs helped
        if (GameManager.Instance != null)
        {
            GameManager.Instance.NPCHelped();
        }

        StartCoroutine(ReturnToMainGame(CorrectSfx.length));
    }
    private IEnumerator ReturnToMainGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MainGame");
    }
}
