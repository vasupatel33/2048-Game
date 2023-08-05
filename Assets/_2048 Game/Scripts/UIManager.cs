using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Samples;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button restartButton;
    public Button optionButton;
    public Button start;

    void Start()
    {
        restartButton.onClick.AddListener(() => { GoogleMobileAdsController.Instance.ShowRewardAd();}) ;
        optionButton.onClick.AddListener(() => { GoogleMobileAdsController.Instance.ShowBannerAdd(); });
        //optionButton.onClick.AddListener(() => { GoogleMobileAdsController.Instance.ShowInterstitialAdd(); });
    }
    void Update()
    {
        
    }
}
