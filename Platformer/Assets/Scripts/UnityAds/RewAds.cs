using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] UnityEngine.UI.Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    string _adUnitId;

    public void Start()
    {
        _adUnitId = _androidAdUnitId;
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.AddListener(ShowAd);
        }
    }
    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            int coins = PlayerPrefs.GetInt("coins");
            coins += 1;
            PlayerPrefs.SetInt("coins", coins);

            Advertisement.Load(_adUnitId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }
}