using System;
using System.Collections;
using UnityEngine;
using VirtueSky.Events;

namespace VirtueSky.Ads
{
    public class Advertising : MonoBehaviour
    {
        [SerializeField] private AdSetting adSetting;

        private IEnumerator autoLoadAdCoroutine;
        private float lastTimeLoadInterstitialAdTimestamp = DEFAULT_TIMESTAMP;
        private float lastTimeLoadRewardedTimestamp = DEFAULT_TIMESTAMP;
        private float lastTimeLoadRewardedInterstitialTimestamp = DEFAULT_TIMESTAMP;
        private float lastTimeLoadAppOpenTimestamp = DEFAULT_TIMESTAMP;
        private const float DEFAULT_TIMESTAMP = -1000;

        private Ads currentAds;

        private void Start()
        {
            switch (adSetting.CurrentAdNetwork)
            {
                case AdNetwork.Applovin:
                    currentAds = adSetting.MaxAds;
                    break;
                case AdNetwork.Admob:
                    currentAds = adSetting.AdmobAds;
                    break;
            }

            currentAds.Initialize();
            autoLoadAdCoroutine = IeAutoLoadAll();
            StartCoroutine(autoLoadAdCoroutine);
        }

        public void OnChangePreventDisplayOpenAd(bool state)
        {
            AdStatic.isShowingAd = state;
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
                yield return new WaitForSeconds(adSetting.AdCheckingInterval);
            }
        }

        #region Func Load Ads

        void AutoLoadInterAds()
        {
            if (Time.realtimeSinceStartup - lastTimeLoadInterstitialAdTimestamp < adSetting.AdLoadingInterval) return;
            currentAds.LoadInterstitial();
            lastTimeLoadInterstitialAdTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadRewardAds()
        {
            if (Time.realtimeSinceStartup - lastTimeLoadRewardedTimestamp < adSetting.AdLoadingInterval) return;
            currentAds.LoadRewarded();
            lastTimeLoadRewardedTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadRewardInterAds()
        {
            if (Time.realtimeSinceStartup - lastTimeLoadRewardedInterstitialTimestamp <
                adSetting.AdLoadingInterval) return;
            currentAds.LoadRewardedInterstitial();
            lastTimeLoadRewardedInterstitialTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadAppOpenAds()
        {
            if (Time.realtimeSinceStartup - lastTimeLoadAppOpenTimestamp < adSetting.AdLoadingInterval) return;
            currentAds.LoadAppOpen();
            lastTimeLoadAppOpenTimestamp = Time.realtimeSinceStartup;
        }

        #endregion
    }
}