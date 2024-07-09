using System.Collections;
using System.Collections.Generic;
#if VIRTUESKY_ADS && VIRTUESKY_ADMOB
using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
#endif
using UnityEngine;


namespace VirtueSky.Ads
{
    public class Advertising : MonoBehaviour
    {
        [SerializeField] private bool dontDestroyOnLoad = true;
        private static Advertising _instance;
        private IEnumerator autoLoadAdCoroutine;
        private float _lastTimeLoadInterstitialAdTimestamp = DEFAULT_TIMESTAMP;
        private float _lastTimeLoadRewardedTimestamp = DEFAULT_TIMESTAMP;
        private float _lastTimeLoadRewardedInterstitialTimestamp = DEFAULT_TIMESTAMP;
        private float _lastTimeLoadAppOpenTimestamp = DEFAULT_TIMESTAMP;
        private const float DEFAULT_TIMESTAMP = -1000;

        private AdClient currentAdClient;
        private AdSettings adSettings;

        private void Awake()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }

            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            adSettings = AdSettings.Instance;
#if VIRTUESKY_IAP
            VirtueSky.Iap.IapManager.OnShowIapNativePopupEvent += OnChangePreventDisplayOpenAd;
#endif
            if (adSettings.EnableGDPR)
            {
#if VIRTUESKY_ADMOB
                InitGDPR();
#endif
            }
            else
            {
                InitAdClient();
            }
        }

        void InitAdClient()
        {
            switch (adSettings.CurrentAdNetwork)
            {
                case AdNetwork.Max:
                    currentAdClient = new MaxAdClient();
                    break;
                case AdNetwork.Admob:
                    currentAdClient = new AdmobClient();
                    break;
                case AdNetwork.IronSource:
                    currentAdClient = new IronSourceClient();
                    break;
            }

            currentAdClient.SetupAdSettings(adSettings);
            currentAdClient.Initialize();
            Debug.Log("currentAdClient: " + currentAdClient);
            InitAutoLoadAds();
        }

        private void InitAutoLoadAds()
        {
            if (autoLoadAdCoroutine != null) StopCoroutine(autoLoadAdCoroutine);
            autoLoadAdCoroutine = IeAutoLoadAll();
            StartCoroutine(autoLoadAdCoroutine);
        }

        IEnumerator IeAutoLoadAll(float delay = 0)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);
            while (true)
            {
                AutoLoadInterAds();
                AutoLoadRewardAds();
                AutoLoadRewardInterAds();
                AutoLoadAppOpenAds();
                yield return new WaitForSeconds(adSettings.AdCheckingInterval);
            }
        }

        private void OnChangePreventDisplayOpenAd(bool state)
        {
            AdStatic.isShowingAd = state;
        }

        #region Method Load Ads

        void AutoLoadInterAds()
        {
            if (Time.realtimeSinceStartup - _lastTimeLoadInterstitialAdTimestamp <
                adSettings.AdLoadingInterval) return;
            currentAdClient.LoadInterstitial();
            _lastTimeLoadInterstitialAdTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadRewardAds()
        {
            if (Time.realtimeSinceStartup - _lastTimeLoadRewardedTimestamp <
                adSettings.AdLoadingInterval) return;
            currentAdClient.LoadRewarded();
            _lastTimeLoadRewardedTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadRewardInterAds()
        {
            if (Time.realtimeSinceStartup - _lastTimeLoadRewardedInterstitialTimestamp <
                adSettings.AdLoadingInterval) return;
            currentAdClient.LoadRewardedInterstitial();
            _lastTimeLoadRewardedInterstitialTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadAppOpenAds()
        {
            if (Time.realtimeSinceStartup - _lastTimeLoadAppOpenTimestamp <
                adSettings.AdLoadingInterval) return;
            currentAdClient.LoadAppOpen();
            _lastTimeLoadAppOpenTimestamp = Time.realtimeSinceStartup;
        }

        #endregion

        #region Admob GDPR

#if VIRTUESKY_ADMOB
        private void InitGDPR()
        {
#if !UNITY_EDITOR
            string deviceID = SystemInfo.deviceUniqueIdentifier;
            string deviceIDUpperCase = deviceID.ToUpper();

            Debug.Log("TestDeviceHashedId = " + deviceIDUpperCase);
            var request = new ConsentRequestParameters { TagForUnderAgeOfConsent = false };
            if (adSettings.EnableGDPRTestMode)
            {
                List<string> listDeviceIdTestMode = new List<string>();
                listDeviceIdTestMode.Add(deviceIDUpperCase);
                request.ConsentDebugSettings = new ConsentDebugSettings
                {
                    DebugGeography = DebugGeography.EEA,
                    TestDeviceHashedIds = listDeviceIdTestMode
                };
            }

            ConsentInformation.Update(request, OnConsentInfoUpdated);
#endif
        }

        private void OnConsentInfoUpdated(FormError consentError)
        {
            if (consentError != null)
            {
                Debug.Log("error consentError = " + consentError);
                return;
            }

            ConsentForm.LoadAndShowConsentFormIfRequired(
                (FormError formError) =>
                {
                    if (formError != null)
                    {
                        Debug.Log("error consentError = " + consentError);
                        return;
                    }

                    Debug.Log("ConsentStatus = " + ConsentInformation.ConsentStatus.ToString());
                    Debug.Log("CanRequestAds = " + ConsentInformation.CanRequestAds());


                    if (ConsentInformation.CanRequestAds())
                    {
                        MobileAds.RaiseAdEventsOnUnityMainThread = true;
                        InitAdClient();
                    }
                }
            );
        }

        private void LoadAndShowConsentForm()
        {
            Debug.Log("LoadAndShowConsentForm Start!");

            ConsentForm.Load((consentForm, loadError) =>
            {
                if (loadError != null)
                {
                    Debug.Log("error loadError = " + loadError);
                    return;
                }


                consentForm.Show(showError =>
                {
                    if (showError != null)
                    {
                        Debug.Log("error showError = " + showError);
                        return;
                    }
                });
            });
        }

        private void ShowPrivacyOptionsForm()
        {
            Debug.Log("Showing privacy options form.");

            ConsentForm.ShowPrivacyOptionsForm((FormError showError) =>
            {
                if (showError != null)
                {
                    Debug.LogError("Error showing privacy options form with error: " + showError.Message);
                }
            });
        }
#endif

        #endregion

#if VIRTUESKY_ADS
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInitialize()
        {
            if (AdSettings.Instance == null) return;
            if (AdSettings.Instance.RuntimeAutoInit)
            {
                var ads = new GameObject("Advertising");
                ads.AddComponent<Advertising>();
                DontDestroyOnLoad(ads);
            }
        }
#endif


        #region Public API

        public static AdUnit BannerAd => _instance.currentAdClient.BannerAdUnit();
        public static AdUnit InterstitialAd => _instance.currentAdClient.InterstitialAdUnit();
        public static AdUnit RewardAd => _instance.currentAdClient.RewardAdUnit();
        public static AdUnit RewardedInterstitialAd => _instance.currentAdClient.RewardedInterstitialAdUnit();
        public static AdUnit AppOpenAd => _instance.currentAdClient.AppOpenAdUnit();
#if VIRTUESKY_ADMOB
        public static void LoadAndShowGDPR() => _instance.LoadAndShowConsentForm();
        public static void ShowAgainGDPR() => _instance.ShowPrivacyOptionsForm();
#endif

        #endregion
    }
}