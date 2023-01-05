using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class CreateWorld
    {
        public static int currentSeed;
        public static int previousSpearSeed;
        private static readonly int[] increments = { 19, 38, 205, 224, 243 };
        private static int incrementIndex = increments.Length; // If this index is outside increments it will just increment by 1
        private static bool hasResetSeedForIncrementingBy1 = true;

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
                ___seed.text = currentSeed.ToString();
            }
        }

        [HarmonyPatch(typeof(SteamLobby), "FindSeed")]
        [HarmonyPostfix]
        private static void getSeed(int __result)
        {
            currentSeed = __result;
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
            isFirstTime = false;
            Debug.Log($"Testing seed {currentSeed}");
        }

        public static void CalculateNextSeed()
        {
            if (previousSpearSeed != currentSeed && incrementIndex >= increments.Length - 1)
            {
                if (!hasResetSeedForIncrementingBy1)
                {
                    currentSeed = previousSpearSeed;
                    hasResetSeedForIncrementingBy1 = true;
                }
                currentSeed++;
                Debug.Log("Incrementing by 1");
                return;
            }

            hasResetSeedForIncrementingBy1 = false;
            incrementIndex = previousSpearSeed == currentSeed ? 0 : incrementIndex + 1;
            currentSeed = previousSpearSeed + increments[incrementIndex];
            Debug.Log($"Incrementing by {increments[incrementIndex]}");
        }
    }
}
