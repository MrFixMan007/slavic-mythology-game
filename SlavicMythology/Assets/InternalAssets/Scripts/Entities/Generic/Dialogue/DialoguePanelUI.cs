using UnityEngine;
using System;
using TMPro;

public class DialoguePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject contentParent;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private GameEventsManager gameEventsManager = GameEventsManager.instance;

    private void OnEnable()
    {
        gameEventsManager ??= GameEventsManager.instance;
        gameEventsManager.dialogueEvents.onDisplayDialogue += DisplayDialogue;
        gameEventsManager.dialogueEvents.onDialogueStarted += DialogueStarted;
        gameEventsManager.dialogueEvents.onDialogueFinished += DialogueFinished;
    }

    private void OnDisable()
    {
        gameEventsManager ??= GameEventsManager.instance;
        gameEventsManager.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
        gameEventsManager.dialogueEvents.onDialogueStarted -= DialogueStarted;
        gameEventsManager.dialogueEvents.onDialogueFinished -= DialogueFinished;
    }

    private void DisplayDialogue(string dialogueLine)
    {
        dialogueText.text = dialogueLine;
    }

    private void DialogueStarted() 
    {
        contentParent.SetActive(true);
    }

    private void DialogueFinished() 
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    private void ResetPanel() 
    {
        dialogueText.text = "";
    }
}