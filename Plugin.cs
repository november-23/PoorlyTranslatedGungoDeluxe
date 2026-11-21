using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace PoorlyTranslatedGungoDeluxe
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "spapi.etg.poorlytranslatedgungodeluxe";
        public const string NAME = "Poorly Translated Gungeon Deluxe";
        public const string VERSION = "1.0.1";
        public static AssetBundle bundle;

        public void Awake()
        {
            var delay = Config.Bind("Delay", "PostInitDelay", true, "When enabled, Poorly Translated Gungeon will wait until all other mods are loaded to apply the language changes.");
            using (var strem = Assembly.GetExecutingAssembly().GetManifestResourceStream("PoorlyTranslatedGungoDeluxe.PoorlyTranslatedGungeonDeluxe.AssetBundles.poorlytranslatedgungodeluxe"))
            {
                bundle = AssetBundle.LoadFromStream(strem);
            }
            if (delay.Value)
            {
                ETGModMainBehaviour.WaitForGameManagerAwake(x =>
                {
                    StartCoroutine(PostStartDelay());
                });
            }
            else
            {
                new Harmony(GUID).PatchAll();
            }
        }

        public IEnumerator PostStartDelay()
        {
            yield return null;
            new Harmony(GUID).PatchAll();
            if (StringTableManager.CurrentLanguage == StringTableManager.GungeonSupportedLanguages.ENGLISH)
            {
                StringTableManager.SetNewLanguage(StringTableManager.CurrentLanguage, true);
            }
            yield break;
        }

        [HarmonyPatch(typeof(BraveResources), nameof(BraveResources.Load), typeof(string), typeof(Type), typeof(string))]
        [HarmonyPrefix]
        public static bool ReplaceFile(ref UnityEngine.Object __result, string path)
        {
            if (path == "strings/english_items/ui")
            {
                __result = bundle.LoadAsset("assets/ui_weird.txt");
                return false;
            }
            else if (path == "strings/english_items/enemies")
            {
                __result = bundle.LoadAsset("assets/enemies_weird.txt");
                return false;
            }
            else if (path == "strings/english")
            {
                __result = bundle.LoadAsset("assets/english_weird.txt");
                return false;
            }
            else if (path == "strings/english_items/intro")
            {
                __result = bundle.LoadAsset("assets/intro_weird.txt");
                return false;
            }
            else if (path == "strings/english_items/items")
            {
                __result = bundle.LoadAsset("assets/items_weird.txt");
                return false;
            }
            else if (path == "strings/english_items/synergies")
            {
                __result = bundle.LoadAsset("assets/synergies_weird.txt");
                return false;
            }
            return true;
        }
    }
}
