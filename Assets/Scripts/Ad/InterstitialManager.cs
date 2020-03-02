
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class InterstitialManager: MonoBehaviour
{
    private static InterstitialAd interstitial;

    private void Start()
    {
        // アプリID
        string appId = "ca-app-pub-8801150864537344~8686480864";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        //RequestBanner();
        RequestInterstitial();
    }

    //インタースティシャル広告のリクエスト
    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ゲーム終了時にインタースティシャル広告を起動
    public static void GameOver()
    {
        if (interstitial.IsLoaded())
        {
            DataBase.isOnline = true;
            Debug.Log("Ad: Interstitial loaded");
            interstitial.Show();
        }
        else
        {
            DataBase.isOnline = false;
        }
    }

    //インタースティシャル広告の削除
    void OnAdClosed(object sender, System.EventArgs e)
    {
        interstitial.Destroy();
    }
}