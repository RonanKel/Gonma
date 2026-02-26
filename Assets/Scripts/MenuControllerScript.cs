using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;



public class MenuControllerScript : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject victoryCard;

    public UnityEvent paused = new UnityEvent();
    public UnityEvent rhythmPaused = new UnityEvent();
    public UnityEvent resumed = new UnityEvent();

    private bool rhythmActive = false;

    

    void Update()
    {
        if (pauseMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeInHierarchy == false)
            {
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
        SceneManager.LoadScene("The Stream");
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("Title Screen");
    }

    public void OpenPauseMenu()
    {
        if (SceneManager.GetActiveScene().name != "Title Screen")
        {
            if (pauseMenu != null)
            {
                if (pauseMenu.activeInHierarchy == false)
                {
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

    }


    public void QuitGameButton()
    {
        Application.Quit();
    }
}
