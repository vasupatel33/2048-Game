using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Samples;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button restartButton;
    public Button optionButton;
    void Start()
    {
        restartButton.onClick.AddListener(() => { GoogleMobileAdsController.Instance.ShowRewardAd();}) ;
        optionButton.onClick.AddListener(() => { GoogleMobileAdsController.Instance.ShowBannerAdd(); });
    }
    void Update()
    {
        
    }
}
