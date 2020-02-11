using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
public class Admob_inter : MonoBehaviour
{
    private InterstitialAd _interstitial;

    // Use this for initialization
    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        // 起動時にインタースティシャル広告をロードしておく
        RequestInterstitial();
    }

    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        // Initialize an InterstitialAd.
        _interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().AddTestDevice("").Build();

        // Load the interstitial with the request.
        _interstitial.LoadAd(request);
    }
    void HandleAdClosed(object sender, System.EventArgs e)
    {
        _interstitial.Destroy();
        RequestInterstitial();
    }
}