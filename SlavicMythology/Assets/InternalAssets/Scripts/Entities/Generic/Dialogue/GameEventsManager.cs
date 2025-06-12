using UnityEngine;
using System;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public InputEvents inputEvents;
    public DialogueEvents dialogueEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        inputEvents = new InputEvents();
        dialogueEvents = new DialogueEvents();
    }
}
