using UnityEngine;

namespace MuckSeedFinder
{
    internal class SeedDifferenceData
    {
        public bool hasFoundItem = false;

        public int previousSeed;
        public readonly int[] increments;
        public int incrementIndex;

        public bool ShouldDemoteMode()
        {
            return !hasFoundItem && incrementIndex >= increments.Length - 1;
        }

        // Returns whether or not it is sure
        public void GoToNextSeed()
        { 
            incrementIndex = hasFoundItem ? 0 : incrementIndex + 1;
            CreateWorld.currentSeed = previousSeed + increments[incrementIndex];
            Debug.Log($"Incrementing by {increments[incrementIndex]}");
        }

        public SeedDifferenceData(int[] increments)
        {
            this.increments = increments;
            incrementIndex = increments.Length;
        }
    }
}
