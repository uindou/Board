using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Admob_inter : MonoBehaviour
{
    private InterstitialAd interStitialView;
    // Use this for initialization
    void Start()
    {
        // アプリID、 これはテスト用
        string appId = "ca-app-pub-8801150864537344~8686480864";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        Debug.Log("Ad loaded");
        RequestInterstitial();
    }
    private void RequestInterstitial()
    {

        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // Create an interstitial at the top of the screen.
        interStitialView = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        interStitialView.LoadAd(request);

    }


    // Update is called once per frame
    void Update()
    {

    }
}