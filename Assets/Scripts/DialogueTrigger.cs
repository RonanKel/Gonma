using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData SDialogue;
    public DialogueData WDialogue;
    public DialogueData LDialogue;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(true, SDialogue);
    }

    public void TriggerWinDialogue()
    {
        DialogueManager.Instance.StartDialogue(false, WDialogue);
    }

    public void TriggerLoseDialogue()
    {
        DialogueManager.Instance.StartDialogue(false, LDialogue);
    }
}
