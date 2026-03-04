using UnityEngine;
using TMPro;

public class JournalScript : MonoBehaviour
{
    private string level_name = "TurtleLevel";
    [SerializeField] GameObject nameText;
    [SerializeField] GameObject scientificNameText;
    [SerializeField] GameObject meetingThoughtsText;
    [SerializeField] GameObject fishImage;
    [SerializeField] GameObject scoreText;

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
    }
}
