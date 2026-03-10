using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;



public class MenuControllerScript : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject victoryCard;

    public UnityEvent paused = new UnityEvent();
    public UnityEvent rhythmPaused = new UnityEvent();
    public UnityEvent resumed = new UnityEvent();

    private bool rhythmActive = false;
    private bool optionOn = false;
    [SerializeField] GameObject Optionmenu;
    [SerializeField] TextMeshProUGUI DelayLabel;
    [SerializeField] TextMeshProUGUI SFXLabel;
    [SerializeField] TextMeshProUGUI MusicLabel;


    

    void Update()
    {
        if (pauseMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy == false)
            {
                Debug.Log("Calling function");
                OpenPauseMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy == true)
            {
                ClosePauseMenu();
            }
        }
    }

    public void SetRhythmActive(bool active)
    {
        rhythmActive = active;
    }

    public void StartGameButton()
    {
        if (PlayerPrefs.HasKey("delay"))
        {
            SceneManager.LoadScene("The Stream");
        }
        else
        {
            SceneManager.LoadScene("Delay Test");
        }
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void OpenPauseMenu()
    {
        if (SceneManager.GetActiveScene().name != "Title Screen")
        {
            Debug.Log("Not title screen");
            if (pauseMenu != null)
            {
                if (pauseMenu.activeInHierarchy == false)
                {
                    Debug.Log("Not active in scene");
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0f;
                    paused.Invoke();
                    if (rhythmActive)
                    {
                        rhythmPaused.Invoke();
                    }

                }
            }
            else
            {
                Debug.LogError("No pause menu assinged to variable in MenuManager");
            }

        }
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu != null)
            {
            if (pauseMenu.activeInHierarchy == true)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                resumed.Invoke();
                }
            }
            else
            {
                Debug.LogError("No pause menu assinged to variable in MenuManager");
            }
    }

    public void OpenVictoryCard()
    {
        if (victoryCard != null) {
            victoryCard.SetActive(true);
        }
    }

    public void CloseVictoryCard()
    {
        if (victoryCard != null) {
            victoryCard.SetActive(false);
        }
    }

    public void ChangeUIIntToText(TextMeshProUGUI textObject, int num)
    {
        textObject.text = num.ToString();
    }

    public void OpenOptionsButton()
    {
        // Optionmenu = GameObject.Find("--Options--");
        // Menu = GameObject.Find("Menu Elements");
        // Set active the options menu
        // disable the buttons in the main menu
        // include a back button in the options menu to return to the main menu
        if (optionOn == false)
        {
            optionOn = true;
            // PlayerPrefs.GetFloat("delay");

            Optionmenu.SetActive(true);
            DelayLabel.text = "Delay: " + PlayerPrefs.GetFloat("delay").ToString("F2") + "s";
            // Menu.SetActive(false);
        }
        else
        {
            optionOn = false;
            Optionmenu.SetActive(false);
            // Menu.SetActive(true);
            }


    }


    public void videomode()
    {
        // Get drop down option
        int mode = GameObject.Find("VideoModeDropdown").GetComponent<TMP_Dropdown>().value;
        if (mode == 0)
        {
            Debug.Log("Fullscreen");
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (mode == 1)
        {
            Debug.Log("Borderless");
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (mode == 2)
        {
            Debug.Log("Windowed");
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void SetDelay()
    {
        // Delay value from slider
        // float delay = GameObject.Find("DelaySlider").GetComponent<Slider>().value;
        // PlayerPrefs.SetFloat("delay", delay);
        float delay = GameObject.Find("DelaySlider").GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("delay", delay);
        DelayLabel.text = "Delay: " + delay.ToString("F2") + "s";
    }

    public void MusicVolume()
    {
        float volume = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
        MusicLabel.text = "Music Volume: " + ((int)(volume * 100)).ToString() + "%";
        // PlayerPrefs.SetFloat("music", volume);
    }

    public void SFXVolume()
    {
        float volume = GameObject.Find("SFXSlider").GetComponent<Slider>().value;
        SFXLabel.text = "SFX Volume: " + ((int)(volume * 100)).ToString() + "%";
        // PlayerPrefs.SetFloat("SFX", volume);
    }


    public void QuitGameButton()
    {
        Application.Quit();
    }
}
