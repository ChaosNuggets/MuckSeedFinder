using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class CreateWorld
    { 
        public static int seed;
        private static bool isFirstTime = true;

        [HarmonyPatch(typeof(MenuUI), "Start")]
        [HarmonyPostfix]
        static void StartLobby()
        {
            SteamManager.Instance.StartLobby();
        }

        [HarmonyPatch(typeof(LobbySettings), "Start")]
        [HarmonyPostfix]
        static void InputSeed(ref TMP_InputField ___seed)
        {
            if (!isFirstTime)
            {
                ___seed.text = seed.ToString();
            }
        }

        [HarmonyPatch(typeof(SteamLobby), "FindSeed")]
        [HarmonyPostfix]
        static void getSeed(int __result)
        {
            seed = __result;
        }

        [HarmonyPatch(typeof(LobbyVisuals), "SpawnLobbyPlayer")]
        [HarmonyPostfix]
        static void StartGame()
        {
            if (!isFirstTime)
            {
                // Without waiting villagers don't spawn in for some reason
                SteamLobby.Instance.StartGame();
                Debug.Log($"Testing seed {seed}");
            }
            isFirstTime = false;
        }
    }
}
