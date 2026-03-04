using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;

    private void OnEnable()
    {
        MusicManagerScript.DialogueBegin += TriggerDialogue;
    }

    private void OnDisable()
    {
        MusicManagerScript.DialogueBegin -= TriggerDialogue;
    }
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
