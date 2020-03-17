using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdMobManager: MonoBehaviour
{
    private float timeOut = 30.1f;
    private float timeElapsed;
    private BannerView bannerView;
    //private static InterstitialAd interstitial;

    private void Start()
    {
        DontDestroyOnLoad(this);

        // アプリID
        string appId = "ca-app-pub-8801150864537344~8686480864";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

    }

    
    //バナー広告のリクエスト
    private void RequestBanner()
    {

        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        // Create a 320x50 banner at the bottom of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);

    }

    // Update is called once per frame
    void Update()
    {
        
        //最初の1フレームおよび30秒に1回バナー広告を更新
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            if (bannerView != null)
            {
                bannerView.Destroy();
                Debug.Log("バナー削除");
            }

            // Do anything
            RequestBanner();
            Debug.Log("バナー更新");
            timeElapsed = 0.1f;
        }

    }

    /*
    //ゲーム終了時にインタースティシャル広告を起動
    public static void GameOver()
    {
        if (interstitial.IsLoaded())
        {
            Debug.Log("Ad: Interstitial loaded");
            interstitial.Show();
        }
    }

    //インタースティシャル広告が削除されたら次をロードする
    void OnAdClosed(object sender, System.EventArgs e)
    {
        interstitial.Destroy();
        Debug.Log("Ad: Interstitial destroyed");
        RequestInterstitial();
    }
    */
}