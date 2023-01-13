using HarmonyLib;
using System;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class Reset
    {
        [HarmonyPatch(typeof(LoadingScreen), "FinishLoading")]
        [HarmonyPrefix]
        private static void ResetWorld()
        {
            if (FindPositions.chiefsSpears.Count > 0)
            {
                double distance = CalculateDistance.CalculateShortestDistance(
                    FindPositions.spawn,
                    FindPositions.chiefsSpears,
                    FindPositions.guardians,
                    FindPositions.boat
                );

                FileStuff.LogSeed(Math.Round(distance));
            }
            ResetVariables();
            GameManager.instance.LeaveGame();
            Debug.Log("Reset world");
        }

        private static void ResetVariables()
        {
            FindPositions.hasFoundBow = false;
            FindPositions.chiefsSpears.Clear();
            FindPositions.guardians.Clear();
            CreateWorld.CalculateNextSeed();
            Debug.Log("Reset variables");
        }
    }
}
