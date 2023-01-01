using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class CreateWorld
    {
        public static int seed = -2147483404;

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
            ___seed.text = seed.ToString();
        }

        [HarmonyPatch(typeof(LobbyVisuals), "SpawnLobbyPlayer")]
        [HarmonyPostfix]
        static void StartGame()
        {
            /*Thread.Sleep(100);*/ // Without waiting villagers don't spawn in for some reason
            SteamLobby.Instance.StartGame();
            Debug.Log($"Testing seed {seed}");
        }
    }
}
