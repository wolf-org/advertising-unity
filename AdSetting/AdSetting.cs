using Sirenix.OdinInspector;
using UnityEngine;

namespace VirtueSky.Ads
{
    public class AdSetting : ScriptableObject
    {
        [Range(5, 100), SerializeField] private float adCheckingInterval = 8f;
        [Range(5, 100), SerializeField] private float adLoadingInterval = 15f;
        [SerializeField] private AdNetwork adNetwork = AdNetwork.Applovin;


        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)] [Header("Applovin Setting")] [SerializeField, TextArea]
        private string sdkKey;

        [ShowIf(nameof(adNetwork), AdNetwork.Applovin)] [SerializeField]
        private ApplovinAds applovinAds;
    }

    public enum AdNetwork
    {
        Applovin,
        Admod
    }
}