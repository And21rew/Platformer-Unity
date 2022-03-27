using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewAd : MonoBehaviour
{
    private string RewardedUnitId = "ca-app-pub-7133832978005389/1311328734";

    private RewardedAd rewardedAd;

    public void Start()
    {
        StartCoroutine(AdReq());
    }
    public void HandleUserEarnedReward(object sender, Reward e)
    {
        int coins = PlayerPrefs.GetInt("coins");
        coins += 1;
        PlayerPrefs.SetInt("coins", coins);
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
            rewardedAd.Show();
    }

    IEnumerator AdReq()
    {
        rewardedAd = new RewardedAd(RewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        yield return new WaitForSecondsRealtime(10f);
        StartCoroutine(AdReq());
    }
}
