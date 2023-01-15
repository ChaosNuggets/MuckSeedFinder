using UnityEngine;

namespace MuckSeedFinder
{
    internal class SeedDifferenceData
    {
        public bool hasFoundItem = false;

        public int previousSeed;
        public readonly int[] increments;
        public int incrementIndex;

        public bool IncrementSeed(out int nextSeed)
        {
            nextSeed = 69420; // This is just to make c# happy lmao, it doesn't do anything

            if (hasFoundItem || incrementIndex < increments.Length - 1)
            {
                incrementIndex = hasFoundItem ? 0 : incrementIndex + 1;
                nextSeed = previousSeed + increments[incrementIndex];
                Debug.Log($"Incrementing by {increments[incrementIndex]}");
                FileStuff.shouldLog = hasFoundItem;
                return true;
            }
            
            if (!CreateWorld.hasResetSeedToPreviousGood)
            {
                nextSeed = previousSeed;
                CreateWorld.hasResetSeedToPreviousGood = true;
            }

            FileStuff.shouldLog = false;
            return false;
        }

        public SeedDifferenceData(int[] increments)
        {
            this.increments = increments;
            incrementIndex = increments.Length;
        }
    }
}