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

        private AdClient currentAdClient;

        private void Start()
        {
            switch (adSetting.CurrentAdNetwork)
            {
                case AdNetwork.Applovin:
                    currentAdClient = adSetting.MaxAdClient;
                    break;
                case AdNetwork.Admob:
                    currentAdClient = adSetting.AdmobAdClient;
                    break;
            }

            currentAdClient.Initialize();
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
            currentAdClient.LoadInterstitial();
            lastTimeLoadInterstitialAdTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadRewardAds()
        {
            if (Time.realtimeSinceStartup - lastTimeLoadRewardedTimestamp < adSetting.AdLoadingInterval) return;
            currentAdClient.LoadRewarded();
            lastTimeLoadRewardedTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadRewardInterAds()
        {
            if (Time.realtimeSinceStartup - lastTimeLoadRewardedInterstitialTimestamp <
                adSetting.AdLoadingInterval) return;
            currentAdClient.LoadRewardedInterstitial();
            lastTimeLoadRewardedInterstitialTimestamp = Time.realtimeSinceStartup;
        }

        void AutoLoadAppOpenAds()
        {
            if (Time.realtimeSinceStartup - lastTimeLoadAppOpenTimestamp < adSetting.AdLoadingInterval) return;
            currentAdClient.LoadAppOpen();
            lastTimeLoadAppOpenTimestamp = Time.realtimeSinceStartup;
        }

        #endregion
    }
}