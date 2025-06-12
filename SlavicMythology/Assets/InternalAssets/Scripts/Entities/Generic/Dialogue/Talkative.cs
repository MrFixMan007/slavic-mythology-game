using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Talkative : MonoBehaviour
{
    [SerializeField] private string dialogueKnotName;
    private bool playerNear = false;
    private GameEventsManager gameEventsManager = GameEventsManager.instance;

    private void OnEnable()
    {
        gameEventsManager ??= GameEventsManager.instance;
        gameEventsManager.inputEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        gameEventsManager ??= GameEventsManager.instance;
        gameEventsManager.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    public void SubmitPressed(InputEventContext inputEventContext) 
    {
        if (!playerNear || !inputEventContext.Equals(InputEventContext.DEFAULT)) 
        {
            return;
        }

        if (!dialogueKnotName.Equals(""))
        {
            GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        }
    }
}
