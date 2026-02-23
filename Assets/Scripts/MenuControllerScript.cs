using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{
    public void StartGameButton() {
        SceneManager.LoadScene("The Stream");
    }

    public void OpenOptionsButton()
    {

    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
