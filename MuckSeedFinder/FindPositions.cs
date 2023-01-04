using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class FindPositions
    {
        public static bool hasFoundBow = false;

        public static Vector3 spawn;
        public static List<Vector3> chiefsSpears = new List<Vector3>();
        public static List<Vector3> guardians = new List<Vector3>();
        public static Vector3 boat;

        [HarmonyPatch(typeof(Chest), "InitChest")]
        [HarmonyPostfix]
        private static void FindWeapons(InventoryItem[] ___cells, Chest __instance)
        {
            // All Chiefs chests have an id of 0 I think
            if (__instance.id != 0) return;

            foreach (InventoryItem item in ___cells)
            {
                if (item == null) continue;

                if (item.name == "Chiefs Spear")
                {
                    chiefsSpears.Add(__instance.transform.position);
                }
                else if (item.name == "Ancient Bow")
                {
                    hasFoundBow = true;
                }
            }
        }

        [HarmonyPatch(typeof(Boat), "Start")]
        [HarmonyPrefix]
        private static void FindBoat(GameObject ___wheel)
        {
            boat = ___wheel.transform.position;
        }

        [HarmonyPatch(typeof(GuardianSpawner), "Start")]
        [HarmonyPostfix]
        private static void FindGuardians(List<GameObject> ___structures)
        {
            foreach (GameObject structure in ___structures)
            {
                guardians.Add(structure.transform.position);
            }
        }

        [HarmonyPatch(typeof(GameManager), "SendPlayersIntoGame")]
        [HarmonyPrefix]
        private static void FindSpawn(List<Vector3> spawnPositions)
        {
            spawn = spawnPositions[0];
        }
    }
}
