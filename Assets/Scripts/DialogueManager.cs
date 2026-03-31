using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private DialogueData CD;
    private int Index;
    private bool SkipRequest = false;

    private bool startingDialogue = false;

    [SerializeField] float typingSpeed;
    private Coroutine typingCoroutine;
    // public static event Action OnDialogueEnded;
    public UnityEvent OnDialogueEnded = new UnityEvent();
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update(){

        // Speedy dialogue
        if (Input.GetKeyDown(KeyCode.Space) && CD != null)
        {
            if (typingCoroutine != null)
            {
                SkipRequest = true;
            }
            else
            {
                NextLine();
            }
        }
    }
    
    public void StartDialogue(bool start, DialogueData dialogue)
    {
        startingDialogue = start;
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
        SkipRequest = false;

        foreach (char letter in text)
        {
            // Now they can skip with space
            if (SkipRequest)
            {
                DialogueUI.Instance.SetDialogueText(text);
                break;
            }
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
        if (startingDialogue) {
            startingDialogue = false;
            OnDialogueEnded.Invoke();
        }else
        {
            GameObject fish = GameObject.Find("Fish");
            fish.SetActive(false);
        }
    }
}
