using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteract : MonoBehaviour
{
    private bool playerNear = false;
    private bool interactingNPC = false;
    private int currentDialogue;

    public GameObject prompt;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public GameObject continuePrompt;

    public string[] dialogues = {"Hello Player!", "Goodbye!"};//array to store dialogue sentence
    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            StartInteract();
        }
        if (interactingNPC && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueInteract();
        }
    }
    void StartInteract()
    {
        interactingNPC = true;
        currentDialogue = 0; //which dialogue the npc at

        prompt.SetActive(false);
        dialoguePanel.SetActive(true);
        continuePrompt.SetActive(true);

        dialogueText.text = dialogues[currentDialogue];
    }
    void ContinueInteract() //change to next sentence of dialogue if have if not end convo
    {
        currentDialogue++;

        if (currentDialogue >= dialogues.Length)
        {
            EndInteract();
        }
        else
        {
            dialogueText.text = dialogues[currentDialogue];
        }
    }
    void EndInteract() //end convo
    {
        interactingNPC = false;
        dialoguePanel.SetActive(false);
        continuePrompt.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("Player is Near");
            prompt.SetActive(true);
            dialoguePanel.SetActive(false); //hide panel
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            Debug.Log("Player is not Near");
            prompt.SetActive(false);
            dialoguePanel.SetActive(false); //hide panel
        }
    }
}
