using HarmonyLib;
using System;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class Reset
    {
        public static bool isResetting = false;

        [HarmonyPatch(typeof(LoadingScreen), "FinishLoading")]
        [HarmonyPrefix]
        public static void ResetWorld()
        {
            if (isResetting) return;

            isResetting = true;
            int nextSeed = CreateWorld.CalculateNextSeed();

            if (FileStuff.shouldLog)
            {
                double distance = CalculateDistance.CalculateShortestDistance(
                    FindPositions.spawn,
                    FindPositions.chiefsSpears,
                    FindPositions.guardians,
                    FindPositions.boat
                );

                FileStuff.LogSeed(Math.Round(distance));
            }
            ResetVariables(nextSeed);
            GameManager.instance.LeaveGame();
            Debug.Log("Reset world");
        }

        private static void ResetVariables(int nextSeed)
        {
            FindPositions.hasFoundBow = false;
            FindPositions.chiefsSpears.Clear();
            FindPositions.guardians.Clear();
            CreateWorld.god.hasFoundItem = false;
            CreateWorld.spear.hasFoundItem = false;
            CreateWorld.currentSeed = nextSeed;
            Debug.Log("Reset variables");
        }
    }
}
