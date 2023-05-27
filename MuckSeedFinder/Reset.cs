using HarmonyLib;
using System;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class Reset
    {
        [HarmonyPatch(typeof(LoadingScreen), "FinishLoading")]
        [HarmonyPrefix]
        public static void ResetWorld()
        {
            if (FindPositions.ChiefsSpears.Count > 0)
            {
                double distance = CalculateDistance.CalculateShortestDistance(
                    FindPositions.Spawn,
                    FindPositions.ChiefsSpears,
                    FindPositions.Guardians,
                    FindPositions.Boat
                );
                Debug.Log($"distance: {distance}");
                //FileStuff.LogSeed(Math.Round(distance));
            }

            FindPositions.ResetVariables();
            GameManager.instance.LeaveGame();
            Debug.Log("Reset world");
        }
    }
}
