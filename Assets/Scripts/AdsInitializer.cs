using UnityEditor.PackageManager;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    //Variables for Initialization
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    //Variables for Showing Interstitial Ads
    [SerializeField] string _androidInterstitialAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsInterstitialAdUnitId = "Interstitial_iOS";
    string _interstitialAdUnitId;

    //Variables for Showing Rewarded Ads
    [SerializeField] Button _showRewardedAdButton;
    [SerializeField] string _androidRewardedAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSRewardedAdUnitId = "Rewarded_iOS";
    string _rewardedAdUnitId;

    void Awake()
    {
        GetInterstitialAdsUnitID();

        //Disable the button until the ad is ready to show:
        _showRewardedAdButton.interactable = false;

        if (Advertisement.isInitialized)
        {
            LoadInterstitialAd();
            LoadRewardedAds();
            Debug.Log("Unity Ads initialization complete.");
        }
        else
        {
            InitializeAds();
        }
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void GetInterstitialAdsUnitID()
    {
        // Get the Ad Unit ID for the current platform:
        _interstitialAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsInterstitialAdUnitId
            : _androidInterstitialAdUnitId;
    }

    public void GetRewardedAdsUnitID()
    {
        // Get the Ad Unit ID for the current platform:
        _rewardedAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSRewardedAdUnitId
            : _androidRewardedAdUnitId;
    }



    // Load content to the Ad Unit:
    public void LoadInterstitialAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Interstitial Ad: " + _interstitialAdUnitId);
        Advertisement.Load(_interstitialAdUnitId, this);
    }

    public void LoadRewardedAds()
    {
        Debug.Log("Loading Rewarded Ad: " + _rewardedAdUnitId);
        Advertisement.Load("Rewarded_Android", this);
    }
    private void Update()
    {
        Debug.Log("Rewarded Ad UnitID"+_rewardedAdUnitId);
        Debug.Log("Game Id: "+_gameId);
    }


    // Show the loaded content in the Ad Unit:
    public void ShowInterstitialAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _interstitialAdUnitId);
        Advertisement.Show(_interstitialAdUnitId, this);
    }

    public void ShowRewardedAds()
    {
        Advertisement.Show(_rewardedAdUnitId, this);
    }

    public void OnInitializationComplete()
    {
        LoadInterstitialAd();
        LoadRewardedAds();
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
        //ShowAd();
        Debug.Log($"Ad Loaded: {_interstitialAdUnitId} ");
        Debug.Log($"Ad Loaded:Rewarded {_rewardedAdUnitId} ");
        _showRewardedAdButton.interactable = true;
        

    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        Debug.Log($"Error loading Ad Unit: {_interstitialAdUnitId} - {error.ToString()} - {message}");
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        Debug.Log($"Error showing Ad Unit {_interstitialAdUnitId}: {error.ToString()} - {message}");
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ads Loaded Completely {_interstitialAdUnitId}: ");
        if(placementId.Equals(_rewardedAdUnitId)&& UnityAdsCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("Reward Player");
        }
    }
}