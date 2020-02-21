using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class BannerManager : MonoBehaviour
{
    private BannerView bannerView;
    // Start is called before the first frame update
    void Start()
    {
        // アプリID
        string appId = "ca-app-pub-8801150864537344~8686480864";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        Debug.Log(bannerView);
        if (bannerView == null)
        {
            RequestBanner();
        }
        
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

    }
}
