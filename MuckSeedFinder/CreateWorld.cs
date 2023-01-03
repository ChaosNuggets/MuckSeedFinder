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
        private static void StartLobby()
        {
            SteamManager.Instance.StartLobby();
        }

        [HarmonyPatch(typeof(LobbySettings), "Start")]
        [HarmonyPostfix]
        private static void InputSeed(ref TMP_InputField ___seed)
        {
            if (!isFirstTime)
            {
                Reset.ResetVariables();
                ___seed.text = seed.ToString();
            }
        }

        [HarmonyPatch(typeof(SteamLobby), "FindSeed")]
        [HarmonyPostfix]
        private static void GetSeed(int __result)
        {
            seed = __result;
        }

        [HarmonyPatch(typeof(LobbyVisuals), "SpawnLobbyPlayer")]
        [HarmonyPostfix]
        private static void StartGame()
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
