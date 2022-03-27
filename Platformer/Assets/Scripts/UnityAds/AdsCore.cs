using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsCore : MonoBehaviour
{
    [SerializeField] private bool _testMode = true;

    private string _gameId = "4496231";

    private string _video = "Interstitial_Android";
    private string _banner = "Banner_Android";

    void Start()
    {
        Advertisement.Initialize(_gameId, _testMode);

        StartCoroutine(ShowBannerWhenInitialized());
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void ShowAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(_video);
        }
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(_banner);
    }
}
