using System;
using GoogleMobileAds.Api;
using UnityEngine;
using VirtueSky.Ads;
using VirtueSky.Global;
#if VIRTUESKY_FIREBASE_ANALYTIC
using Firebase.Analytics;
#endif

#if VIRTUESKY_ADJUST
using com.adjust.sdk;
#endif

namespace VirtueSky.Ads
{
    public class AdmobAdClient : AdClient
    {
        public override void Initialize()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            MobileAds.Initialize(_ =>
            {
                App.RunOnMainThread(() =>
                {
                    if (!adSetting.AdmobEnableTestMode) return;
                    var configuration = new RequestConfiguration { TestDeviceIds = adSetting.AdmobDevicesTest };
                    MobileAds.SetRequestConfiguration(configuration);
                });
            });
            adSetting.AdmobBannerVariable.paidedCallback = TrackRevenue;
            adSetting.AdmobInterVariable.paidedCallback = TrackRevenue;
            adSetting.AdmobRewardVariable.paidedCallback = TrackRevenue;
            adSetting.AdmobRewardInterVariable.paidedCallback = TrackRevenue;
            adSetting.AdmobAppOpenVariable.paidedCallback = TrackRevenue;
            RegisterAppStateChange();
            LoadInterstitial();
            LoadRewarded();
            LoadRewardedInterstitial();
            LoadAppOpen();
#endif
        }

        public override void LoadInterstitial()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            if (!IsInterstitialReady()) adSetting.AdmobInterVariable.Load();
#endif
        }

        public override bool IsInterstitialReady()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            return adSetting.AdmobInterVariable.IsReady();
#else
            return false;
#endif
        }

        public override void LoadRewarded()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            if (!IsRewardedReady()) adSetting.AdmobRewardVariable.Load();
#endif
        }

        public override bool IsRewardedReady()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            return adSetting.AdmobRewardVariable.IsReady();
#else
            return false;
#endif
        }

        public override void LoadRewardedInterstitial()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            if (!IsRewardedInterstitialReady()) adSetting.AdmobRewardInterVariable.Load();
#endif
        }

        public override bool IsRewardedInterstitialReady()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            return adSetting.AdmobRewardInterVariable.IsReady();
#else
            return false;
#endif
        }

        public override void LoadAppOpen()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            if (!IsAppOpenReady()) adSetting.AdmobAppOpenVariable.Load();
#endif
        }

        public override bool IsAppOpenReady()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            return adSetting.AdmobAppOpenVariable.IsReady();
#else
            return false;
#endif
        }

        void ShowAppOpen()
        {
#if VIRTUESKY_ADS && ADS_ADMOB
            if (statusAppOpenFirstIgnore) adSetting.AdmobAppOpenVariable.Show();
            statusAppOpenFirstIgnore = true;
#endif
        }
#if VIRTUESKY_ADS && ADS_ADMOB
        void RegisterAppStateChange()
        {
            GoogleMobileAds.Api.AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        }

        void OnAppStateChanged(GoogleMobileAds.Common.AppState state)
        {
            if (state == GoogleMobileAds.Common.AppState.Foreground)
            {
                if (adSetting.CurrentAdNetwork == AdNetwork.Admob) ShowAppOpen();
            }
        }
#endif

        void TrackRevenue(double value, string network, string unitId, string format)
        {
#if VIRTUESKY_ADJUST
            AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
            adjustAdRevenue.setRevenue(value, "USD");
            adjustAdRevenue.setAdRevenueNetwork(network);
            adjustAdRevenue.setAdRevenueUnit(unitId);
            adjustAdRevenue.setAdRevenuePlacement(format);
            Adjust.trackAdRevenue(adjustAdRevenue);
#endif
#if VIRTUESKY_FIREBASE_ANALYTIC
            Parameter[] parameters =
            {
                new("value", value),
                new("ad_platform", "AppLovin"),
                new("ad_format", format),
                new("currency", "USD"),
                new("ad_unit_name", unitId),
                new("ad_source", network)
            };

            FirebaseAnalytics.LogEvent("ad_impression", parameters);
#endif
        }
    }
}