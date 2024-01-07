using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Sample;
using GoogleMobileAds.Samples;
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
        SceneManager.LoadScene("2048");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
