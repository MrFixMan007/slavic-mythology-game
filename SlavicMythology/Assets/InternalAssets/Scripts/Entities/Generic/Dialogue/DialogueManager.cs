using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;
    [SerializeField] private SceneEnd TheEnd;

    private Story story;

    private bool dialoguePlaying = false;

    private GameEventsManager gameEventsManager = GameEventsManager.instance;

    private void Awake()
    {
        story = new Story(inkJson.text);
    }

    private void OnEnable()
    {
        gameEventsManager ??= GameEventsManager.instance;
        gameEventsManager.dialogueEvents.onEnterDialogue += EnterDialogue;
        gameEventsManager.inputEvents.onSubmitPressed += SubmitPressed;
    }

    private void OnDisable()
    {
        gameEventsManager ??= GameEventsManager.instance;
        gameEventsManager.dialogueEvents.onEnterDialogue -= EnterDialogue;
        gameEventsManager.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    private void SubmitPressed(InputEventContext inputEventContext) 
    {
        if (!inputEventContext.Equals(InputEventContext.DIALOGUE))
        {
            return;
        }

        ContinueOrExitStory();
    }

    private void EnterDialogue(string knotName) //--------------------------------ADD DISABLE PLAYER MOVEMENT, rewrite inputs to Event Driven Arch---------------------------------------
    {
        if (dialoguePlaying)
        {
            ContinueOrExitStory();//just return if it worked as intended
            return;
        }

        dialoguePlaying = true;

        gameEventsManager.dialogueEvents.DialogueStarted();

        gameEventsManager.inputEvents.ChangeInputEventContext(InputEventContext.DIALOGUE);

        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.Log("empty knot");
        }

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory() 
    {
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            Debug.Log(dialogueLine);
            gameEventsManager.dialogueEvents.DisplayDialogue(dialogueLine);
        }
        else
        {
            Time.timeScale = 0f;
            StartCoroutine(TheEnd.AnimateSceneEnd());
            ExitDialogue();
        }
    }

    private void ExitDialogue() //-----------------ENABLE MOVEMENT-------------------
    {
        dialoguePlaying = false;

        gameEventsManager.dialogueEvents.DialogueFinished();

        gameEventsManager.inputEvents.ChangeInputEventContext(InputEventContext.DEFAULT);

        story.ResetState();
    }
}