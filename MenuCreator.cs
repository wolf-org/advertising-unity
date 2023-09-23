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
        [MenuItem("Tools/AdSetting %E", false)]
        public static void MenuOpenAdSettings()
        {
            var adSetting = AssetUtils.FindAssetAtFolder<AdSetting>(new string[] { "Assets" }).FirstOrDefault();
            if (adSetting == null)
            {
                ScriptableSetting.CreateSettingAssets<AdSetting>();
                adSetting = AssetUtils.FindAssetAtFolder<AdSetting>(new string[] { "Assets" }).FirstOrDefault();
            }

            Selection.activeObject = adSetting;
            EditorUtility.FocusProjectWindow();
        }

        public static T CreateScriptableSetting<T>() where T : ScriptableObject
        {
            var so = AssetUtils.FindAssetAtFolder<T>(new string[] { "Assets" }).FirstOrDefault();
            if (so == null)
            {
                ScriptableSetting.CreateSettingAssets<T>();
                so = AssetUtils.FindAssetAtFolder<T>(new string[] { "Assets" }).FirstOrDefault();
            }
            return so;
        }
    }
}