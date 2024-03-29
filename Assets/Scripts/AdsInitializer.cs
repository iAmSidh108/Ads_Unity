using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    //Variables for Initialization
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] RewardedAdsButton rewardedAdsButton;
    [SerializeField] InterstitialAdsButton interstitialAdsButton;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }
  
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        rewardedAdsButton.LoadAd();
        interstitialAdsButton.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    
}