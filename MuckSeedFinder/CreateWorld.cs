using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class CreateWorld
    {
        public static int currentSeed;
        public static SeedDifferenceData spear = new SeedDifferenceData(
            new int[] { 19, 38, 205, 224, 243 }
        );
        public static SeedDifferenceData god = new SeedDifferenceData(
            new int[] { 19, 38, 1204, 1223, 1242, 1261, 1280, 1299, 7745, 7764, 9025, 9044, 9063 }
        ); // This is the seed data for the seeds with spear + ancient bow

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
            if (god.IncrementSeed(ref currentSeed)) return;
            if (spear.IncrementSeed(ref currentSeed)) return;
            currentSeed++;
            Debug.Log("Incrementing by 1");
        }
    }
}
