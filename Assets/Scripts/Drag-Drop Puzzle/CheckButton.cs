using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckButton : MonoBehaviour
{
    public ClearButton clearButton; //reference to ClearButton Script

    public PuzzleData sentenceData; //Sentence for the puzzle
    public Transform wordSpawnArea; //Where the words will spawn
    public Transform dropWordsArea; //Where the words will be dropped to form sentences
    public GameObject wordPrefab;

    //Audio
    public AudioSource audiosource;
    public AudioClip CorrectSfx; //correct sound effects when player got puzzle correct
    public AudioClip WrongSfx; //wrong sound effects when player gets puzzzle wrong

    private bool puzzleCompleted = false; //stop puzzle from being completed again
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

        //Shuffle the words in the list
        List<string> shuffledWords = new List<string>(sentenceData.Sentence);

        for (int i = 0; i < shuffledWords.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledWords.Count);
            string word = shuffledWords[i];
            shuffledWords[i] = shuffledWords[randomIndex];
            shuffledWords[randomIndex] = word;
        }

        //Spawn the words showing text on it
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

        //get the text in the word button
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
            return;
        }

        //Check whether the seentence have any word in wrong order by iterating through it
        for (int i = 0; i < sentenceData.Sentence.Count; i++)
        {
            if (droppedWords[i] != sentenceData.Sentence[i])
            {
                //if there is words in the wrong spot the wrong sound effects will play
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

        //play correct sound effects when player got it correct
        if (audiosource && CorrectSfx)
            audiosource.PlayOneShot(CorrectSfx);

        //Tell GameManager that puzzle is solved and NPC is helped
        if (GameManager.Instance != null)
        {
            GameManager.Instance?.NPCHelped(); //count NPC helped
        }

        //Wait for the correct sound effect is played finished before going back to MainGame scene
        StartCoroutine(ReturnToMainGame(CorrectSfx.length));
    }
    private IEnumerator ReturnToMainGame(float waitTime)
    {
        //delay abit before going back to MainGame Scene
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MainGame");
    }
}
