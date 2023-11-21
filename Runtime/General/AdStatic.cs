using System;
using UnityEngine;

namespace VirtueSky.Ads
{
    public static class AdStatic
    {
        public static bool IsRemoveAd
        {
            get => GetBool($"{Application.identifier}_removeads", false);
            set => SetBool($"{Application.identifier}_removeads", value);
        }

        private static bool GetBool(string key, bool defaultValue = false) =>
            PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) > 0;

        private static void SetBool(string id, bool value) => PlayerPrefs.SetInt(id, value ? 1 : 0);

        internal static bool isShowingAd;

        public static AdUnitVariable OnDisplayed(this AdUnitVariable unit, Action onDisplayed)
        {
            unit.displayedCallback = onDisplayed;
            return unit;
        }

        public static AdUnitVariable OnClosed(this AdUnitVariable unit, Action onClosed)
        {
            unit.closedCallback = onClosed;
            return unit;
        }

        public static AdUnitVariable OnLoaded(this AdUnitVariable unit, Action onLoaded)
        {
            unit.loadedCallback = onLoaded;
            return unit;
        }

        public static AdUnitVariable OnFailedToLoad(this AdUnitVariable unit, Action onFailedToLoad)
        {
            unit.failedToLoadCallback = onFailedToLoad;
            return unit;
        }

        public static AdUnitVariable OnFailedToDisplay(this AdUnitVariable unit, Action onFailedToDisplay)
        {
            unit.failedToDisplayCallback = onFailedToDisplay;
            return unit;
        }

        public static AdUnitVariable OnCompleted(this AdUnitVariable unit, Action onCompleted)
        {
            switch (unit)
            {
                case AdmobInterVariable admobInter:
                    admobInter.completedCallback = onCompleted;
                    return unit;
                case AdmobRewardVariable admobReward:
                    admobReward.completedCallback = onCompleted;
                    return unit;
                case AdmobRewardInterVariable admobRewardInter:
                    admobRewardInter.completedCallback = onCompleted;
                    return unit;
                case MaxInterVariable maxInter:
                    maxInter.completedCallback = onCompleted;
                    return unit;
                case MaxRewardVariable maxReward:
                    maxReward.completedCallback = onCompleted;
                    return unit;
                case MaxRewardInterVariable maxRewardInter:
                    maxRewardInter.completedCallback = onCompleted;
                    return unit;
            }

            return unit;
        }

        public static AdUnitVariable OnSkipped(this AdUnitVariable unit, Action onSkipped)
        {
            switch (unit)
            {
                case AdmobRewardVariable admobReward:
                    admobReward.skippedCallback = onSkipped;
                    return unit;
                case AdmobRewardInterVariable admobRewardInter:
                    admobRewardInter.skippedCallback = onSkipped;
                    return unit;
                case MaxRewardVariable maxReward:
                    maxReward.skippedCallback = onSkipped;
                    return unit;
                case MaxRewardInterVariable maxRewardInter:
                    maxRewardInter.skippedCallback = onSkipped;
                    return unit;
            }

            return unit;
        }
    }
}