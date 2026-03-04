using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    public GameObject dialoguePanel;
    public GameObject speakerText;
    public GameObject dialogueText;
    public GameObject nextText;

    private TextMeshProUGUI ST;
    private TextMeshProUGUI DT;


    void Start()
    {
        ST = speakerText.GetComponent<TextMeshProUGUI>();
        DT = dialogueText.GetComponent<TextMeshProUGUI>();
        HideDialogueUI();
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void ShowDialogueUI()
    {
        dialoguePanel.SetActive(true);
        speakerText.SetActive(true);
        dialogueText.SetActive(true);
    }

    public void HideDialogueUI()
    {
        dialoguePanel.SetActive(false);
        speakerText.SetActive(false);
        dialogueText.SetActive(false);
        nextText.SetActive(false);
    }

    public void UpdateUI(string speaker, string text)
    {
        ST.text = speaker;
        DT.text = text;
    }

    public void AppendCharacter(char character)
    {
        DT.text += character;
    }

    public void SetSpeaker(string speaker)
    {
        ST.text = speaker;
    }

    public void SetDialogueText(string text)
    {
        DT.text = text;
    }
    public void SetNext(int count)
    {
        if (count == 1){
            nextText.SetActive(true);
        }
        else
        {
            nextText.SetActive(false);
        }
    }

    // public void OnClickNext()
    // {
    //     Debug.Log("Clicked!");
    //     DialogueManager.Instance.NextLine();
    // }
}