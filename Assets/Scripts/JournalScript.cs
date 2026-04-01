using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JournalScript : MonoBehaviour
{
    private string level_name = "TurtleLevel";
    [SerializeField] GameObject fishLogUI;
    [SerializeField] GameObject buffUI;


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
    [SerializeField] GameObject pageNumberText;

    private int curr_page = 0;
    [SerializeField] List<Level> pages;

    [SerializeField] Material greyOut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetJournalObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            FlipPageBackward();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            FlipPageForward();
        }
    }

    public void FlipPageForward()
    {
        if (curr_page < pages.Count - 1) {
            curr_page++;
            SetJournalObjects();
        }
    }

    public void FlipPageBackward()
    {
        if (curr_page > -1) {
            curr_page--;
            SetJournalObjects();
        }
    }

    void OnEnable()
    {
        SetJournalObjects();
    }

    void SetJournalObjects()
    {
        SetJournalObjectsDeactive();
        pageNumberText.GetComponent<TextMeshProUGUI>().text = (curr_page + 1).ToString();
        if (curr_page == -1)
        {
            fishLogUI.SetActive(false);
            buffUI.SetActive(true);
            return;
        }
        fishLogUI.SetActive(true);
        buffUI.SetActive(false);

        Level level = pages[curr_page];
        string level_name = level.name;
        fishImage.SetActive(true);
        fishImage.GetComponent<Image>().sprite = level.fishSprite;
        fishImage.GetComponent<Image>().material = greyOut;
        
        if (PlayerPrefs.HasKey(level_name))
        {
            scoreText.SetActive(true);
            scoreText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(level_name).ToString();
           
            nameText.SetActive(true);
            nameText.GetComponent<TextMeshProUGUI>().text = level.fishname;
            
            fishImage.SetActive(true);
            fishImage.GetComponent<Image>().sprite = level.fishSprite;
            if (level.angryFishSprite != null)
            {
                fishImage.GetComponent<Image>().sprite = level.angryFishSprite;
            }
            fishImage.GetComponent<Image>().material = null;
            
            meetingThoughtsText.SetActive(true);
            meetingThoughtsText.GetComponent<TextMeshProUGUI>().text = level.meetingThoughts;
        }
        if (PlayerPrefs.HasKey(level_name + "award1")) {
            if (PlayerPrefs.GetInt(level_name + "award1") == 1)
            {
                descriptionText.SetActive(true);
                descriptionText.GetComponent<TextMeshProUGUI>().text = level.description;
                award1Image.SetActive(true);
                award1Image.GetComponent<Image>().sprite = level.award1;
                trinketImage.SetActive(true);
                trinketImage.GetComponent<Image>().sprite = level.trinketSprite;
                fishImage.GetComponent<Image>().sprite = level.fishSprite;
                scientificNameText.SetActive(true);
            scientificNameText.GetComponent<TextMeshProUGUI>().text = level.scientificFishname;
            }
        }
        if (PlayerPrefs.HasKey(level_name + "award2")) {
            if (PlayerPrefs.GetInt(level_name + "award2") == 1)
            {
                award2Image.SetActive(true);
                award2Image.GetComponent<Image>().sprite = level.award2;
                extra1Text.SetActive(true);
                extra1Text.GetComponent<TextMeshProUGUI>().text = level.extra1;
            }
        }
        if (PlayerPrefs.HasKey(level_name + "award3")) {
            if (PlayerPrefs.GetInt(level_name + "award3") == 1)
            {
                award3Image.SetActive(true);
                award3Image.GetComponent<Image>().sprite = level.award3;
                extra2Text.SetActive(true);
                extra2Text.GetComponent<TextMeshProUGUI>().text = level.extra2;
            }
        }
    }

    void SetJournalObjectsDeactive()
    {
        nameText.SetActive(false);
        scientificNameText.SetActive(false);
        meetingThoughtsText.SetActive(false);
        fishImage.SetActive(false);
        descriptionText.SetActive(false);
        scoreText.SetActive(false);
        award1Image.SetActive(false);
        award2Image.SetActive(false);
        award3Image.SetActive(false);
        trinketImage.SetActive(false);
        extra1Text.SetActive(false);
        extra2Text.SetActive(false);
    }

    public void SetPage(int pageNum)
    {
        curr_page = pageNum;
        SetJournalObjects();
    }
}
