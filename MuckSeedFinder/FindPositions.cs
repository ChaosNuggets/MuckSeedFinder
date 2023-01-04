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
        static void FindWeapons(InventoryItem[] ___cells, Chest __instance)
        {
            // All Chiefs chests have an id of 0 I think
            if (__instance.id != 0) return;

            foreach (InventoryItem item in ___cells)
            {
                if (item == null) continue;

                if (item.name == "Chiefs Spear")
                {
                    chiefsSpears.Add(__instance.transform.position);
                    Debug.Log($"Found chiefs spear at {__instance.transform.position}");
                }
                else if (item.name == "Ancient Bow")
                {
                    hasFoundBow = true;
                    Debug.Log($"Found ancient bow at {__instance.transform.position}");
                }
            }

            if (chiefsSpears.Count == 0)
            {
                Reset.ResetWorld();
            }
        }

        [HarmonyPatch(typeof(Boat), "Start")]
        [HarmonyPrefix]
        static void FindBoat(GameObject ___wheel)
        {
            boat = ___wheel.transform.position;
            Debug.Log($"Found boat at {___wheel.transform.position}");
        }

        [HarmonyPatch(typeof(GuardianSpawner), "Start")]
        [HarmonyPostfix]
        static void FindGuardians(List<GameObject> ___structures)
        {
            foreach (GameObject structure in ___structures)
            {
                guardians.Add(structure.transform.position);
                Debug.Log($"Found guardian at {structure.transform.position}");
            }
        }

        [HarmonyPatch(typeof(GameManager), "SendPlayersIntoGame")]
        [HarmonyPrefix]
        static void FindSpawn(List<Vector3> spawnPositions)
        {
            spawn = spawnPositions[0];
            Debug.Log($"Found spawn at {spawnPositions[0]}");
            Reset.ResetWorld();
        }
    }
}
