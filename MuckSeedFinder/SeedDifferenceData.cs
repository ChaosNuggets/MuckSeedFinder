using UnityEngine;

namespace MuckSeedFinder
{
    internal class SeedDifferenceData
    {
        public bool hasFoundItem = false;

        public int previousSeed;
        public readonly int[] increments;
        public int incrementIndex;

        private bool hasResetSeedToPreviousGood = false;

        public bool IncrementSeed(out int nextSeed)
        {
            if (hasFoundItem || incrementIndex < increments.Length - 1)
            {
                incrementIndex = hasFoundItem ? 0 : incrementIndex + 1;
                nextSeed = previousSeed + increments[incrementIndex];
                Debug.Log($"Incrementing by {increments[incrementIndex]}");
                FileStuff.shouldLog = hasFoundItem;
                return true;
            }
            
            if (!hasResetSeedToPreviousGood)
            {
                nextSeed = previousSeed;
                hasResetSeedToPreviousGood = true;
            }
            FileStuff.shouldLog = false;
            nextSeed = 69420; // This is just to make c# happy lmao, it doesn't do anything
            return false;
        }

        public SeedDifferenceData(int[] increments)
        {
            this.increments = increments;
            incrementIndex = increments.Length;
        }
    }
}