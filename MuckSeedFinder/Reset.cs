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

            if (FileStuff.ShouldLog())
            {
                double distance = CalculateDistance.CalculateShortestDistance(
                    FindPositions.spawn,
                    FindPositions.chiefsSpears,
                    FindPositions.guardians,
                    FindPositions.boat
                );

                FileStuff.LogSeed(Math.Round(distance));
            }
            CreateWorld.GoToNextSeed();
            ResetVariables();
            GameManager.instance.LeaveGame();
            Debug.Log("Reset world");
        }

        private static void ResetVariables()
        {
            FindPositions.hasFoundBow = false;
            FindPositions.chiefsSpears.Clear();
            FindPositions.guardians.Clear();
            CreateWorld.god.hasFoundItem = false;
            CreateWorld.spear.hasFoundItem = false;
            Debug.Log("Reset variables");
        }
    }
}
