using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using VirtueSky.EditorUtils;

namespace VirtueSky.Ads
{
    public class AdSetting : ScriptableObject
    {
        [SerializeField] private bool autoInit = true;
        [Range(5, 100), SerializeField] private float adCheckingInterval = 8f;
        [Range(5, 100), SerializeField] private float adLoadingInterval = 15f;
        [SerializeField] private AdNetwork adNetwork = AdNetwork.Applovin;

        public bool AutoInit => autoInit;
        public float AdCheckingInterval => adCheckingInterval;
        public float AdLoadingInterval => adLoadingInterval;

        public AdNetwork CurrentAdNetwork
        {
            get => adNetwork;
            set => adNetwork = value;
        }

        #region AppLovin

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)] [Header("Applovin")] [SerializeField, TextArea]
        private string sdkKey;

        #region Max Ads

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)] [InlineButton(nameof(CreateMaxAds), "Create")] [SerializeField]
        private MaxAds maxAds;

        void CreateMaxAds()
        {
            maxAds = ScriptableSetting.CreateAndGetScriptableAsset<MaxAds>();
        }

        public MaxAds MaxAds => maxAds;

        #endregion

        #region Max Banner

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)]
        [InlineButton(nameof(CreateMaxBanner), "Create")]
        [SerializeField]
        private MaxBannerVariable maxBannerVariable;

        void CreateMaxBanner()
        {
            maxBannerVariable = ScriptableSetting.CreateAndGetScriptableAsset<MaxBannerVariable>();
        }

        public MaxBannerVariable MaxBannerVariable => maxBannerVariable;

        #endregion

        #region Max Inter

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)]
        [InlineButton(nameof(CreateMaxInter), "Create")]
        [SerializeField]
        private MaxInterVariable maxInterVariable;

        void CreateMaxInter()
        {
            maxInterVariable = ScriptableSetting.CreateAndGetScriptableAsset<MaxInterVariable>();
        }

        public MaxInterVariable MaxInterVariable => maxInterVariable;

        #endregion

        #region Max Reward

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)]
        [InlineButton(nameof(CreateMaxReward), "Create")]
        [SerializeField]
        private MaxRewardVariable maxRewardVariable;

        void CreateMaxReward()
        {
            maxRewardVariable = ScriptableSetting.CreateAndGetScriptableAsset<MaxRewardVariable>();
        }

        public MaxRewardVariable MaxRewardVariable => maxRewardVariable;

        #endregion

        #region Max RewardInter

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)]
        [InlineButton(nameof(CreateMaxRewardInter), "Create")]
        [SerializeField]
        private MaxRewardInterVariable maxRewardInterVariable;

        void CreateMaxRewardInter()
        {
            maxRewardInterVariable = ScriptableSetting.CreateAndGetScriptableAsset<MaxRewardInterVariable>();
        }

        public MaxRewardInterVariable MaxRewardInterVariable => maxRewardInterVariable;

        #endregion

        #region Max AppOpen

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)]
        [InlineButton(nameof(CreateMaxAppOpen), "Create")]
        [SerializeField]
        private MaxAppOpenVariable maxAppOpenVariable;

        void CreateMaxAppOpen()
        {
            maxAppOpenVariable = ScriptableSetting.CreateAndGetScriptableAsset<MaxAppOpenVariable>();
        }

        public MaxAppOpenVariable MaxAppOpenVariable => maxAppOpenVariable;

        #endregion

        #endregion

        #region Admob

        #region Admob Ads

        [ShowIf(nameof(adNetwork), AdNetwork.Admob)]
        [Header("Admob")]
        [InlineButton(nameof(CreateAdmodAds), "Create")]
        [SerializeField]
        private AdmobAds admobAds;

        void CreateAdmodAds()
        {
            admobAds = ScriptableSetting.CreateAndGetScriptableAsset<AdmobAds>();
        }

        public AdmobAds AdmobAds => admobAds;

        #endregion

        #region Admod Banner

        [ShowIf(nameof(adNetwork), AdNetwork.Admob)]
        [InlineButton(nameof(CreateAdmobBanner), "Create")]
        [SerializeField]
        private AdmobBannerVariable admobBannerVariable;

        void CreateAdmobBanner()
        {
            admobBannerVariable = ScriptableSetting.CreateAndGetScriptableAsset<AdmobBannerVariable>();
        }

        #endregion

        #region Admod Inter

        [ShowIf(nameof(adNetwork), AdNetwork.Admob)] [InlineButton(nameof(CreateAdmobInter), "Create")] [SerializeField]
        private AdmobInterVariable admobInterVariable;

        void CreateAdmobInter()
        {
            admobInterVariable = ScriptableSetting.CreateAndGetScriptableAsset<AdmobInterVariable>();
        }

        public AdmobInterVariable AdmobInterVariable => admobInterVariable;

        #endregion

        #region Admod Reward

        [ShowIf(nameof(adNetwork), AdNetwork.Admob)]
        [InlineButton(nameof(CreateAdmobReward), "Create")]
        [SerializeField]
        private AdmobRewardVariable admobRewardVariable;

        void CreateAdmobReward()
        {
            admobRewardVariable = ScriptableSetting.CreateAndGetScriptableAsset<AdmobRewardVariable>();
        }

        #endregion

        #region Admod RewardInter

        [ShowIf(nameof(adNetwork), AdNetwork.Admob)]
        [InlineButton(nameof(CreateAdmobRewardInter), "Create")]
        [SerializeField]
        private AdmobRewardInterVariable admobRewardInterVariable;

        void CreateAdmobRewardInter()
        {
            admobRewardInterVariable = ScriptableSetting.CreateAndGetScriptableAsset<AdmobRewardInterVariable>();
        }

        public AdmobRewardInterVariable AdmobRewardInterVariable => admobRewardInterVariable;

        #endregion

        #region Admod AppOpen

        [ShowIf(nameof(adNetwork), AdNetwork.Admob)]
        [InlineButton(nameof(CreateAdmobAppOpen), "Create")]
        [SerializeField]
        private AdmobAppOpenVariable admobAppOpenVariable;

        void CreateAdmobAppOpen()
        {
            admobAppOpenVariable = ScriptableSetting.CreateAndGetScriptableAsset<AdmobAppOpenVariable>();
        }

        public AdmobAppOpenVariable AdmobAppOpenVariable => admobAppOpenVariable;

        #endregion

        #endregion
    }

    public enum AdNetwork
    {
        Applovin,
        Admob
    }
}