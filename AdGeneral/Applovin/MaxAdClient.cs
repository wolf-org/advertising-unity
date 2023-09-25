using UnityEngine;
#if VIRTUESKY_FIREBASE_ANALYTIC
using Firebase.Analytics;
#endif

#if VIRTUESKY_ADJUST
using com.adjust.sdk;
#endif

namespace VirtueSky.Ads
{
    public class MaxAdClient : AdClient
    {
        public override void Initialize()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            MaxSdk.SetSdkKey(adSetting.SdkKey);
            MaxSdk.InitializeSdk();
            MaxSdk.SetIsAgeRestrictedUser(adSetting.ApplovinEnableAgeRestrictedUser);
            adSetting.MaxBannerVariable.paidedCallback = TrackRevenue;
            adSetting.MaxInterVariable.paidedCallback = TrackRevenue;
            adSetting.MaxRewardVariable.paidedCallback = TrackRevenue;
            adSetting.MaxRewardInterVariable.paidedCallback = TrackRevenue;
            adSetting.MaxAppOpenVariable.paidedCallback = TrackRevenue;
            LoadInterstitial();
            LoadRewarded();
            LoadRewardedInterstitial();
            LoadAppOpen();
#endif
        }

        public override void LoadInterstitial()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            if (!IsInterstitialReady()) adSetting.MaxInterVariable.Load();
#endif
        }

        public override bool IsInterstitialReady()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            return adSetting.MaxInterVariable.IsReady();
#else
            return false;
#endif
        }

        public override void LoadRewarded()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            if (!IsRewardedReady()) adSetting.MaxRewardVariable.Load();
#endif
        }

        public override bool IsRewardedReady()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            return adSetting.MaxRewardVariable.IsReady();
#else
            return false;
#endif
        }

        public override void LoadRewardedInterstitial()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            if (!IsRewardedInterstitialReady()) adSetting.MaxRewardInterVariable.Load();
#endif
        }

        public override bool IsRewardedInterstitialReady()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            return adSetting.MaxRewardInterVariable.IsReady();
#else
            return false;
#endif
        }

        public override void LoadAppOpen()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            if (!IsAppOpenReady()) adSetting.MaxAppOpenVariable.Load();
#endif
        }

        public override bool IsAppOpenReady()
        {
#if VIRTUESKY_ADS && ADS_APPLOVIN
            return adSetting.MaxAppOpenVariable.IsReady();
#else
            return false;
#endif
        }

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