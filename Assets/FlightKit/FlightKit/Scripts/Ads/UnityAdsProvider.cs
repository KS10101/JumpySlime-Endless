using System;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

namespace FlightKit
{
#if !UNITY_ADS
    public class UnityAdsProvider : AbstractAdsProvider
    {
        public override void ShowRewardedAd(Action<AdShowResult> resultCallback)
        {
            resultCallback(AdShowResult.Finished);
        }
    }
#else
    public class UnityAdsProvider : AbstractAdsProvider, IUnityAdsInitializationListener, IUnityAdsLoadListener,
        IUnityAdsShowListener
    {
        [SerializeField]
        string androidGameId;
        [SerializeField]
        string iOSGameId;
        [SerializeField]
        bool testMode = true;

        [Space]
        [SerializeField]
        string androidAdUnitId = "Interstitial_Android";
        [SerializeField]
        string iOSAdUnitId = "Interstitial_iOS";

        private string _gameId;
        private string _adUnitId;
        private Action<AdShowResult> _resultCallback;

        public override void ShowRewardedAd(Action<AdShowResult> resultCallback)
        {
            _resultCallback = resultCallback;

            if (!Advertisement.isInitialized)
            {
                resultCallback(AdShowResult.Failed);
                return;
            }

            ShowAd();
        }

        void Awake()
        {
            InitializeAds();
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSAdUnitId
                : androidAdUnitId;
        }

        public void InitializeAds()
        {
#if UNITY_IOS
            _gameId = iOSGameId;
#elif UNITY_ANDROID
            _gameId = androidGameId;
#elif UNITY_EDITOR
            _gameId = androidGameId; //Only for testing the functionality in the Editor
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, testMode, this);
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public void ShowAd()
        {
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            _resultCallback(showCompletionState == UnityAdsShowCompletionState.COMPLETED
                ? AdShowResult.Finished
                : AdShowResult.Skipped);
        }
    }
#endif
}