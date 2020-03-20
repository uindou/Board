using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class RewardAdManager:MonoBehaviour
{
    public GameObject rewardWindow;
    public GameObject waitWindow;
    public GameObject errorWindow;
    public GameObject confirmWindow;
    public GameObject timeLimitPanel;
    private DateTime startTime;

    //AndroidAdUnit ID
    private string AndroidUnitId = "ca-app-pub-3940256099942544/5224354917";
    
    //RewardVideoAd.
    private RewardBasedVideoAd RewardAd =null;

    protected string AdUnitID =null;

    // Start
    void Start()
    {
        requestRewardAd();
    }

// OnDestroy
    void　OnDestroy()
    {

    }

    public bool IsActive{private　set;get;}
    
    //requestRewardMovie Ad。
    private void requestRewardAd()
    {
        AdUnitID = AndroidUnitId;

        this.RewardAd = RewardBasedVideoAd.Instance;

        RewardAd.OnAdLoaded += OnAdLoaded;
        RewardAd.OnAdFailedToLoad += OnAdLoadFailed;
        RewardAd.OnAdStarted += OnAdStarted;
        RewardAd.OnAdClosed += OnAdClosed;
        RewardAd.OnAdRewarded += OnAdRewarded;
        RewardAd.OnAdLeavingApplication += OnAdLeavingApp;

    }

    public void LoadStart()
    {
        confirmWindow.SetActive(false);
        waitWindow.SetActive(true);
        AdRequest　request = new　AdRequest.Builder().Build();
        this.RewardAd.LoadAd(request, AdUnitID);

        IsActive = true;
    }

    protected　void　OnAdLoaded(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdLoaded");
        //Justincase.
        if (RewardAd.IsLoaded() == true)
        {
            RewardAd.Show();
            timeLimitPanel.SetActive(true);
            startTime = System.DateTime.Now;
            PlayerPrefs.SetString("rewardAdLimitStart", startTime.ToBinary().ToString());
        }

        else
        {
            waitWindow.SetActive(false);
            errorWindow.SetActive(true);
        }
    }

    protected　void　OnAdLoadFailed(object　_sender, AdFailedToLoadEventArgs　_args)
    {
        Debug.Log("AdLoadFailed");
        waitWindow.SetActive(false);
        errorWindow.SetActive(true);
        IsActive = true;
    }

    protected　void　OnAdStarted(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdStarted");
        IsActive = true;
    }

    protected　void　OnAdClosed(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdClosed");
        waitWindow.SetActive(false);
        IsActive = false;
    }

    protected　void　OnAdRewarded(object　_sender, Reward　_args)
    {
        Debug.Log("AdRewarded!!!");

        if (_args != null)
        {
            Debug.Log("AdRewardedRewardType[" + _args.Type + "]");
            Debug.Log("AdRewardedRewardAmount[" + _args.Amount.ToString() + "]");
        }


        CoinManager.SetBonus(200);
        CoinManager.SetCoin();

        waitWindow.SetActive(false);
        rewardWindow.SetActive(true);
        IsActive = true;
    }

    protected　void　OnAdLeavingApp(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdLeavingApplication");
        IsActive = false;
    }
}