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
            double distance = CalculateDistance.CalculateShortestDistance(
                FindPositions.Spawn,
                FindPositions.ChiefsSpears,
                FindPositions.Guardians,
                FindPositions.Boat
            );
            FileStuff.LogSeed(Math.Round(distance));

            FindPositions.ResetVariables();
            GameManager.instance.LeaveGame();
            Debug.Log("Reset world");
        }
    }
}
