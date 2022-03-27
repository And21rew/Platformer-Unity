using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdBanner : MonoBehaviour
{
    private BannerView bannerView;
    private string bannerUnitId = "ca-app-pub-7133832978005389/6844452901";

    public void OnEnable()
    {
        bannerView = new BannerView(bannerUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
        StartCoroutine(ShowBanner());
    }

    IEnumerator ShowBanner()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        bannerView.Show();
    }
}
