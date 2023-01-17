using HarmonyLib;
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
            if (__instance.id != 0 || Reset.isResetting) return;

            foreach (InventoryItem item in ___cells)
            {
                if (Reset.isResetting) return;

                if (item == null) continue;

                if (item.name == "Chiefs Spear")
                {
                    chiefsSpears.Add(__instance.transform.position);
                    CreateWorld.spear.previousSeed = CreateWorld.currentSeed;
                    CreateWorld.spear.hasFoundItem = true;
                    if (CreateWorld.currentMode < CreateWorld.Mode.Spear)
                    {
                        CreateWorld.currentMode = CreateWorld.Mode.Spear;
                    }
                    Debug.Log($"Found chiefs spear at {__instance.transform.position}");
                }
                else if (item.name == "Ancient Bow")
                {
                    hasFoundBow = true;
                    Debug.Log($"Found ancient bow at {__instance.transform.position}");
                }
            }

            if (hasFoundBow && CreateWorld.spear.hasFoundItem)
            {
                CreateWorld.god.previousSeed = CreateWorld.currentSeed;
                CreateWorld.god.hasFoundItem = true;
                CreateWorld.currentMode = CreateWorld.Mode.God;
                return;
            }

            ResetEarlyIfShould();
        }

        private static void ResetEarlyIfShould()
        {
            if (!CreateWorld.ShouldFastReset) return;

            if (CreateWorld.currentMode == CreateWorld.Mode.God)
            {
                if (!CreateWorld.god.hasFoundItem)
                {
                    Reset.ResetWorld();
                }
            }
            else if (CreateWorld.currentMode == CreateWorld.Mode.Spear)
            {
                if (!CreateWorld.spear.hasFoundItem)
                {
                    Reset.ResetWorld();
                }
            }
            else
            {
                Reset.ResetWorld();
            }
        }

        [HarmonyPatch(typeof(Boat), "Start")]
        [HarmonyPrefix]
        private static void FindBoat(GameObject ___wheel)
        {
            boat = ___wheel.transform.position;
            Debug.Log($"Found boat at {___wheel.transform.position}");
        }

        [HarmonyPatch(typeof(GuardianSpawner), "Start")]
        [HarmonyPostfix]
        private static void FindGuardians(List<GameObject> ___structures)
        {
            foreach (GameObject structure in ___structures)
            {
                guardians.Add(structure.transform.position);
                Debug.Log($"Found guardian at {structure.transform.position}");
            }
        }

        [HarmonyPatch(typeof(GameManager), "SendPlayersIntoGame")]
        [HarmonyPrefix]
        private static void FindSpawn(List<Vector3> spawnPositions)
        {
            spawn = spawnPositions[0];
            Debug.Log($"Found spawn at {spawnPositions[0]}");
        }
    }
}
