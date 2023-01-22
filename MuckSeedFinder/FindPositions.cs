using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class FindPositions
    {
        public static bool HasFoundBow { get; private set; } = false;

        public static Vector3 Spawn { get; private set; }
        private static List<Vector3> chiefsSpears = new List<Vector3>();
        public static IList<Vector3> ChiefsSpears
        {
            get { return chiefsSpears.AsReadOnly(); }
        }
        private static List<Vector3> guardians = new List<Vector3>();
        public static IList<Vector3> Guardians
        {
            get { return guardians.AsReadOnly(); }
        }
        public static Vector3 Boat { get; private set; }

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
                    Debug.Log($"Found chiefs spear at {__instance.transform.position}");
                }
                else if (item.name == "Ancient Bow")
                {
                    HasFoundBow = true;
                    Debug.Log($"Found ancient bow at {__instance.transform.position}");
                }
            }

            if (!HasFoundBow || chiefsSpears.Count == 0)
            {
                Debug.LogError($"{CreateWorld.CurrentSeed} is not a god seed for some reason?");
            }
        }

        [HarmonyPatch(typeof(Boat), "Start")]
        [HarmonyPrefix]
        private static void FindBoat(GameObject ___wheel)
        {
            Boat = ___wheel.transform.position;
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
            Spawn = spawnPositions[0];
            Debug.Log($"Found spawn at {spawnPositions[0]}");
        }

        public static void ResetVariables()
        {
            HasFoundBow = false;
            chiefsSpears.Clear();
            guardians.Clear();
        }
    }
}
