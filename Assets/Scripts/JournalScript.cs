using UnityEngine;
using TMPro;

public class JournalScript : MonoBehaviour
{
    private string level_name = "TurtleLevel";
    [SerializeField] GameObject nameText;
    [SerializeField] GameObject scientificNameText;
    [SerializeField] GameObject meetingThoughtsText;
    [SerializeField] GameObject fishImage;
    [SerializeField] GameObject descriptionText;
    [SerializeField] GameObject scoreText;
    [SerializeField] GameObject award1Image;
    [SerializeField] GameObject award2Image;
    [SerializeField] GameObject award3Image;
    [SerializeField] GameObject trinketImage;
    [SerializeField] GameObject extra1Text;
    [SerializeField] GameObject extra2Text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey(level_name))
        {
            scoreText.SetActive(true);
            scoreText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(level_name).ToString();
            nameText.SetActive(true);
            scientificNameText.SetActive(true);
            fishImage.SetActive(true);
            meetingThoughtsText.SetActive(true);
        }
        if (PlayerPrefs.HasKey(level_name + "award1")) {
            if (PlayerPrefs.GetInt(level_name + "award1") == 1)
            {
                descriptionText.SetActive(true);
                award1Image.SetActive(true);
                trinketImage.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey(level_name + "award2")) {
            if (PlayerPrefs.GetInt(level_name + "award2") == 1)
            {
                award2Image.SetActive(true);
                extra1Text.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey(level_name + "award3")) {
            if (PlayerPrefs.GetInt(level_name + "award3") == 1)
            {
                award3Image.SetActive(true);
                extra2Text.SetActive(true);
            }
        }
    }
}
