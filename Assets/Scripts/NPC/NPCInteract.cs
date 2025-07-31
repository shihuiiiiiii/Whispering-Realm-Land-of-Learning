using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    private bool playerNear = false;
    private bool interactingNPC = false;
    private int currentDialogue;

    //Dialogue
    public GameObject prompt;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public GameObject continuePrompt;

    //Data of dialogue content
    public DialogueLines dialogueLines;
    public DialogueChoiceData dialogueChoiceData;
    public int ChoiceTriggerLine = 2;

    //Choice
    public GameObject choicePanel;
    public GameObject choiceButtonPrefab;
    public bool isChoosing = false;

    void Start()  //Set everything to be not active first coz convo have not start
    {
        prompt.SetActive(false);
        dialoguePanel.SetActive(false);
        continuePrompt.SetActive(false);
        choicePanel.SetActive(false);
    }
    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            StartInteract();
        }
        if (interactingNPC && Input.GetKeyDown(KeyCode.Space) && !isChoosing)
        {
            ContinueInteract();
        }
    }
    void StartInteract()
    {
        interactingNPC = true;
        currentDialogue = 0; //which dialogue line the npc at

        prompt.SetActive(false);
        dialoguePanel.SetActive(true);
        continuePrompt.SetActive(true);
        choicePanel.SetActive(false);

        dialogueText.text = dialogueLines.Lines[currentDialogue];
    }
    void ContinueInteract() //change to next sentence of dialogue if have if not end convo
    {
        currentDialogue++;

        if (ChoiceTriggerLine >= 0 && currentDialogue == ChoiceTriggerLine)
        {
            ShowChoices();
            return;
        }

        if (currentDialogue >= dialogueLines.Lines.Length)
        {
            EndInteract();
        }
        else
        {
            dialogueText.text = dialogueLines.Lines[currentDialogue];
        }
    }

    //Spawn Choice buttons
    void ShowChoices()
    {
        isChoosing = true;
        choicePanel.SetActive(true);
        continuePrompt.SetActive(false);

        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject); //need to destroy the buttons so when the dialogue trigger again there wont be extra of the same buttons spawned from previous interaction
        }

        foreach (DialogueChoice choice in dialogueChoiceData.Choices)
        {
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choicePanel.transform);
            TMP_Text buttonText = choiceButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = choice.PlayerChoice;

            choiceButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                ChoiceSelected(choice);
            });
        }
    }

    //after choice selected, close the UI buttons
    void ChoiceSelected(DialogueChoice choice)
    {
        isChoosing = false;
        dialogueText.text = choice.NpcResponse; //show npc response
        choicePanel.SetActive(false ); //hide panel after choosing the choice
        continuePrompt.SetActive(true);
    }

    //End Dialogue (close all the ui panels)
    void EndInteract()
    {
        interactingNPC = false;
        dialoguePanel.SetActive(false);
        continuePrompt.SetActive(false);
    }

    //Check when player in range of NPC
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
    //Check when player out of range of NPC
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