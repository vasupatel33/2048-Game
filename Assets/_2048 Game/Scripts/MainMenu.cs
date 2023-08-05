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

        //SceneManager.LoadScene(firstLevel);
        //For check internet
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            InterstitialAdController.OnUserClosed = null;
            InterstitialAdController.OnUserClosed += () => { SceneManager.LoadScene("2048"); };
            GoogleMobileAdsController.Instance.ShowInterstitialAdd();
        }
        else
        {
            SceneManager.LoadScene("2048");
        }
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
