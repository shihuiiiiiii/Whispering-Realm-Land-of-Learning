using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckButton : MonoBehaviour
{
    public SentenceData sentenceData;
    public Transform wordSpawnArea;
    public Transform dropWordsArea;
    public GameObject wordPrefab;
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
        foreach (string word in sentenceData.Sentence)
        {
            GameObject spawnWord = Instantiate(wordPrefab, wordSpawnArea);
            spawnWord.GetComponentInChildren<TMPro.TMP_Text>().text = word;
        }
    }
    public void CheckAnswer()
    {
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
                return;
            }
        }
        Debug.Log("Correct Answer");
    }
}
