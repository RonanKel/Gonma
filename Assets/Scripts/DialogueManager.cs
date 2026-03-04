using UnityEngine;
using System.Collections;
using System;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private DialogueData CD;
    private int Index;

    public float typingSpeed = 0.1f;
    private Coroutine typingCoroutine;
    public static event Action OnDialogueEnded;
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void StartDialogue(DialogueData dialogue)
    {
        CD = dialogue;
        Index = 0;
        DialogueUI.Instance.ShowDialogueUI();
        DisplayCurrentLine();
    }

    public void NextLine()
    {
        Index++;
        if (Index >= CD.lines.Count)
        {
            EndDialogue();
        }
        else{
            DisplayCurrentLine();
        }
    }

    void DisplayCurrentLine()
    {
        DialogueLine line = CD.lines[Index];

        DialogueUI.Instance.SetSpeaker(line.speakerName);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        DialogueUI.Instance.SetNext(0);
        typingCoroutine = StartCoroutine(TypeLine(line.dialogueText));
    }

    IEnumerator TypeLine(string text)
    {
        DialogueUI.Instance.SetDialogueText("");

        foreach (char letter in text)
        {
            DialogueUI.Instance.AppendCharacter(letter);
            yield return new WaitForSeconds(typingSpeed);
        }

        typingCoroutine = null;
        DialogueUI.Instance.SetNext(1);
    }
    void EndDialogue()
    {
        DialogueUI.Instance.HideDialogueUI();
        CD = null;
        OnDialogueEnded?.Invoke();
    }
}
