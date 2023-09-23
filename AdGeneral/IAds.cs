using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtueSky.Ads
{
    public interface IAds
    {
        void Initialize();
        void LoadInterstitial();
        bool IsInterstitialReady();
        void LoadRewarded();
        bool IsRewardedReady();
        void LoadRewardedInterstitial();
        bool IsRewardedInterstitialReady();
        void LoadAppOpen();
        bool IsAppOpenReady();
    }
}