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
    private bool IsAdPlayed;

    //AndroidAdUnit ID
    private string AndroidUnitId = "ca-app-pub-8801150864537344/9098420115";
    
    //RewardVideoAd.
    private RewardBasedVideoAd RewardAd =null;

    protected string AdUnitID =null;

    // Start
    void Start()
    {
        IsAdPlayed = false;
        requestRewardAd();
    }

    public bool IsActive{private　set;get;}
    
    //リワード広告をリクエスト
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

    //広告の読み込みおよび再生
    public void LoadStart()
    {
        confirmWindow.SetActive(false);
        waitWindow.SetActive(true);
        AdRequest　request = new　AdRequest.Builder().Build();
        this.RewardAd.LoadAd(request, AdUnitID);
        Invoke("LoadButNotPlay", 7.0f);
        IsActive = true;
    }

    //広告が読み込まれた後に実行
    protected　void　OnAdLoaded(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdLoaded");

        //念のため分岐
        if (RewardAd.IsLoaded() == true)
        {
            RewardAd.Show();
        }
        else
        {
            waitWindow.SetActive(false);
            errorWindow.SetActive(true);
        }
    }

    //読み込みに失敗したときに実行
    protected　void　OnAdLoadFailed(object　_sender, AdFailedToLoadEventArgs　_args)
    {
        Debug.Log("AdLoadFailed");
        waitWindow.SetActive(false);
        errorWindow.SetActive(true);
        IsActive = true;
    }

    //広告が再生したときに実行
    protected　void　OnAdStarted(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdStarted");
        IsActive = true;
        IsAdPlayed = true;
    }

    //広告が終了前に閉じられたときに実行
    protected　void　OnAdClosed(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdClosed");
        waitWindow.SetActive(false);
        IsActive = false;
    }

    //広告を最後まで見たときに実行
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

        timeLimitPanel.SetActive(true);
        startTime = System.DateTime.Now;
        PlayerPrefs.SetString("rewardAdLimitStart", startTime.ToBinary().ToString());

        waitWindow.SetActive(false);
        rewardWindow.SetActive(true);
        IsActive = true;
    }

    //広告再生中にアプリを閉じたときなどに実行
    protected　void　OnAdLeavingApp(object　_sender, System.EventArgs　_args)
    {
        Debug.Log("AdLeavingApplication");
        IsActive = false;
    }

    //広告がロードされても流れないバグ対策。数秒後にwaitWindowを強制的に閉じる。
    protected void LoadButNotPlay()
    {
        waitWindow.SetActive(false);
    }
}