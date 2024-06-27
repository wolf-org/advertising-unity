using System.Collections.Generic;

#if VIRTUESKY_APPSFLYER
using AppsFlyerSDK;
#endif


namespace VirtueSky.Ads
{
    public struct AppsFlyerTrackingRevenue
    {
        public static void AppsFlyerTrackRevenueAd(double value, string network, string unitId,
            string format, string adNetwork)
        {
#if VIRTUESKY_APPSFLYER
            var mediationNetworks = AppsFlyerAdRevenueMediationNetworkType
                .AppsFlyerAdRevenueMediationNetworkTypeGoogleAdMob;
            switch (adNetwork.ToLower())
            {
                case "admob":
                    mediationNetworks = AppsFlyerAdRevenueMediationNetworkType
                        .AppsFlyerAdRevenueMediationNetworkTypeGoogleAdMob;
                    break;
                case "max":
                    mediationNetworks = AppsFlyerAdRevenueMediationNetworkType
                        .AppsFlyerAdRevenueMediationNetworkTypeApplovinMax;
                    break;
            }

            Dictionary<string, string> additionalParams = new Dictionary<string, string>();
            additionalParams.Add(AFAdRevenueEvent.COUNTRY, "US");
            additionalParams.Add(AFAdRevenueEvent.AD_UNIT, unitId);
            additionalParams.Add(AFAdRevenueEvent.AD_TYPE, format);
            AppsFlyerAdRevenue.logAdRevenue(network,
                mediationNetworks,
                value,
                "USD",
                additionalParams);
#endif
        }
    }
}