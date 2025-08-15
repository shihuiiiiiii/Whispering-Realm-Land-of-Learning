using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    [Header("NPC Settings")]
    private bool playerNear = false; //is player close enough to NPC to interact
    private bool interactingNPC = false; //is player interacting with NPC
    private int currentDialogue; //the current line of Dialogue the player is at
    public bool hasBeenHelped = false; //if the player has helped the NPC

    //Dialogue
    [Header("Dialogue UI")]
    public GameObject prompt; //"Press E to talk" prmt on NPC
    public GameObject dialoguePanel; //NPC dialogue Panel
    public TMP_Text dialogueText; //dialogue text in dialogue panel
    public GameObject continuePrompt; //"Press space to continue"

    //Data of dialogue content
    [Header("Dialogue Data")]
    public DialogueLines dialogueLines; //dialogue lines for the NPC
    public DialogueChoiceData dialogueChoiceData; //data for choices player can pick
    public int ChoiceTriggerLine = 2; //which line will the choice options be triggered

    //Data of the Puzzle
    [Header("Puzzle Data")]
    public bool havePuzzle = false; //does NPC have puzzle on them
    public PuzzleData puzzleData; //the puzzle data for the npc if they have

    //Choice
    [Header("Choice UI")]
    public GameObject choicePanel; //panel for the choices
    public GameObject choiceButtonPrefab; //choice button prefab
    public bool isChoosing = false; //if player is choosing the choice
    public bool TriggerPuzzleChoice = false; //whether it is a choice that will trigger the puzzle

    void Start()  //Set everything to be not active to hide because dialogue have not start
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
        if (interactingNPC && !isChoosing && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueInteract();
        }
    }
    void StartInteract()
    {
        interactingNPC = true;
        //currentDialogue = 0; //which dialogue line the npc at

        if (DialogueManager.dialogueManager.dialogueNum == 0)
        {
            currentDialogue = 0;
        }
        else if (DialogueManager.dialogueManager.dialogueNum == 2)
        {
            currentDialogue = 3;
        }

        prompt.SetActive(false);
        dialoguePanel.SetActive(true);
        continuePrompt.SetActive(true);
        choicePanel.SetActive(false);

        dialogueText.text = dialogueLines.Lines[currentDialogue];
    }
    void ContinueInteract() //change to next sentence of dialogue if have if not end conversation
    {
        currentDialogue++;

        //if this dialogue line will trigger choice or not to show the choice panel
        if (ChoiceTriggerLine >= 0 && currentDialogue == ChoiceTriggerLine)
        {
            ShowChoices();
            return;
        }
        //End dialogue if finished
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
            DialogueChoice currentChoice = choice; //capture players choice
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choicePanel.transform);
            TMP_Text buttonText = choiceButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = choice.PlayerChoice;

            choiceButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                ChoiceSelected(choice);
                Debug.Log("Choice Selected:"+ choice.PlayerChoice);

                if (havePuzzle && choice.TriggersPuzzleChoice)
                {
                    Debug.Log("Loading Puzzle" + this.puzzleData.name);
                    PuzzleManager.Instance.puzzleData = this.puzzleData;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SentencePuzzle");
                }
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
            interactingNPC = false; //reset dialogue state
            currentDialogue = 0; //restart dialogue
            isChoosing = false;
            Debug.Log("Player is not Near");

            if (prompt != null)
            {
                prompt.SetActive(false);
            }
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false); //hide panel
            }
        }
    }
    public void CompleteHelp()
    {
        if (!hasBeenHelped)
        {
            hasBeenHelped = true;
            GameManager.Instance.NPCsHelped++;
        }
    }
}