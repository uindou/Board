using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Admob: MonoBehaviour
{
    private float timeOut = 30.0f;
    private float timeElapsed;
    private BannerView bannerView;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        // アプリID、 これはテスト用
        string appId = "ca-app-pub-8801150864537344~8686480864";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        Debug.Log("Ad loaded");
        RequestBanner();
    }
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
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            if (bannerView != null)
            {
                bannerView.Destroy();
                Debug.Log("Ad destroyed");
            }

            // Do anything
            RequestBanner();
            Debug.Log("Ad loaded");
            timeElapsed = 0.0f;
        }
    }
}