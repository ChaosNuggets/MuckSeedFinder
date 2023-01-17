using HarmonyLib;
using Steamworks;
using System.Threading;
using TMPro;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class CreateWorld
    {
        public static int currentSeed;

        public const bool ShouldFastReset = true;

        public enum Mode
        {
            One,
            Spear,
            God
        }
        public static Mode currentMode = Mode.One;
        public static SeedDifferenceData spear = new SeedDifferenceData(new int[]
        {
            19, 38,
            205, 224, 243
        });
        public static SeedDifferenceData god = new SeedDifferenceData(new int[]
        {
            19, 38, 57,
            1204, 1223, 1242, 1261, 1280, 1299,
            7745, 7764,
            9025, 9044, 9063,
            //10419, 10438, 10457, 10476, 10495, // I've never actually seen 10457 but I have a suspicion that it exists
            //11699,
            //16903, 16922
        }); // This is the seed data for the seeds with spear + ancient bow

        private static bool isFirstTime = true;

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
        private static void GetSeed(int __result)
        {
            currentSeed = __result;

            if (isFirstTime)
            {
                god.previousSeed = currentSeed;
                spear.previousSeed = currentSeed;
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
            isFirstTime = false;
            Debug.Log($"Testing seed {currentSeed}");
            Reset.isResetting = false;
        }

        public static void GoToNextSeed()
        {
            if (currentMode == Mode.God)
            {
                if (god.ShouldDemoteMode())
                {
                    currentSeed = god.previousSeed;   
                    spear.previousSeed = currentSeed;
                    spear.incrementIndex = -1;
                    currentMode--;
                    Debug.Log("Demoting mode");
                }
                else
                {
                    god.GoToNextSeed();
                    Debug.Log("Finding god seeds");
                }
            }
            if (currentMode == Mode.Spear)
            {
                if (spear.ShouldDemoteMode())
                {
                    currentSeed = spear.previousSeed;
                    currentMode--;
                    Debug.Log("Demoting mode");
                }
                else
                {
                    spear.GoToNextSeed();
                    Debug.Log("Finding spear seeds");
                }
            }
            if (currentMode == Mode.One)
            {
                currentSeed++;
                Debug.Log("Incrementing by 1");
            }
        }
    }
}
