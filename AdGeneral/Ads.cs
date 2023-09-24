using Sirenix.OdinInspector;
using UnityEngine;

namespace VirtueSky.Ads
{
    public abstract class Ads : ScriptableObject
    {
        [SerializeField, ReadOnly] protected AdSetting adSetting;
        protected bool statusAppOpenFirstIgnore;

        public void SetupAdSetting(AdSetting _adSetting)
        {
            this.adSetting = _adSetting;
        }

        public abstract void Initialize();
        public abstract void LoadInterstitial();
        public abstract bool IsInterstitialReady();
        public abstract void LoadRewarded();
        public abstract bool IsRewardedReady();
        public abstract void LoadRewardedInterstitial();
        public abstract bool IsRewardedInterstitialReady();
        public abstract void LoadAppOpen();
        public abstract bool IsAppOpenReady();
    }
}