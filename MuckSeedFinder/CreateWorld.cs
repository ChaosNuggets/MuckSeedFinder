using HarmonyLib;
using Steamworks;
using System.Threading;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class CreateWorld
    {
        public static int CurrentSeed { get; private set; }
        private static bool isFirstTime = true;
        private static SeedCalculator seedCalculator;

        [HarmonyPatch(typeof(MenuUI), "Start")]
        [HarmonyPostfix]
        private static void StartLobby()
        {
            SteamManager.Instance.StartLobby();
        }

        [HarmonyPatch(typeof(SteamManager), "OnLobbyCreatedCallback")]
        [HarmonyPostfix]
        private static void RetryStart(Result result)
        {
            if (result != Result.OK)
            {
                Thread.Sleep(5000);
                StatusMessage.Instance.OkayDokay(); // Press the okay dokay button
                StartLobby();
            }
        }

        [HarmonyPatch(typeof(LobbyVisuals), "SpawnLobbyPlayer")]
        [HarmonyPostfix]
        private static void StartGame()
        {
            if (!isFirstTime)
            {
                // Without waiting villagers don't spawn in for some reason
                SteamLobby.Instance.StartGame();
            }
        }

        [HarmonyPatch(typeof(SteamLobby), "FindSeed")]
        [HarmonyPostfix]
        private static void InputSeed(ref int __result)
        {
            if (isFirstTime)
            {
                seedCalculator = new SeedCalculator(__result);
            }
            __result = seedCalculator.CalculateNextGodSeed();
            CurrentSeed = __result;
            Debug.Log($"Testing seed {CurrentSeed}");
        }

        [HarmonyPatch(typeof(SteamLobby), "StartGame")]
        [HarmonyPostfix]
        private static void SetIsFirstTimeToFalse()
        {
            isFirstTime = false;
        }
    }
}
