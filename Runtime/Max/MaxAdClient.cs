namespace VirtueSky.Ads
{
    public class MaxAdClient : AdClient
    {
        public override void Initialize()
        {
#if VIRTUESKY_ADS && VIRTUESKY_MAX
            MaxSdk.SetSdkKey(adSettings.SdkKey);
            MaxSdk.InitializeSdk();
            MaxSdk.SetIsAgeRestrictedUser(adSettings.ApplovinEnableAgeRestrictedUser);
            adSettings.MaxBannerAdUnit.Init();
            adSettings.MaxInterstitialAdUnit.Init();
            adSettings.MaxRewardAdUnit.Init();
            adSettings.MaxAppOpenAdUnit.Init();
            adSettings.MaxRewardedInterstitialAdUnit.Init();

#if VIRTUESKY_TRACKING
            adSettings.MaxBannerAdUnit.paidedCallback = VirtueSky.Tracking.AppTracking.TrackRevenue;
            adSettings.MaxInterstitialAdUnit.paidedCallback = VirtueSky.Tracking.AppTracking.TrackRevenue;
            adSettings.MaxRewardAdUnit.paidedCallback = VirtueSky.Tracking.AppTracking.TrackRevenue;
            adSettings.MaxRewardedInterstitialAdUnit.paidedCallback = VirtueSky.Tracking.AppTracking.TrackRevenue;
            adSettings.MaxAppOpenAdUnit.paidedCallback = VirtueSky.Tracking.AppTracking.TrackRevenue;

#endif

            LoadInterstitial();
            LoadRewarded();
            LoadRewardedInterstitial();
            LoadAppOpen();
#endif
        }
    }
}