using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public GameObject optionsScreen;

    public GameObject loadingScreen, loadingIcon;
    public Text loadingText;

    void Start()
    {

    }

    void Update()
    {

    }

    public void StartGame()
    {
        //SceneManager.LoadScene(firstLevel);
        SceneManager.LoadScene("2048");
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
