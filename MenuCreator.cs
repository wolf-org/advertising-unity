using System.Linq;
using UnityEditor;
using UnityEngine;
using VirtueSky.Core;
using VirtueSky.EditorUtils;
using VirtueSky.Utils;

namespace VirtueSky.Ads
{
    public class MenuCreator : EditorWindow
    {
        [MenuItem("GameControl/AdSetting %E", false)]
        public static void MenuOpenAdSettings()
        {
            var adSetting = ScriptableSetting.CreateAndGetScriptableAsset<VirtueSky.Ads.AdSetting>("/Ads");
            Selection.activeObject = adSetting;
            EditorUtility.FocusProjectWindow();
        }
    }
}