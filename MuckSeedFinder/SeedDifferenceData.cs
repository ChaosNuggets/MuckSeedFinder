using UnityEngine;

namespace MuckSeedFinder
{
    internal class SeedDifferenceData
    {
        public int previousSeed;
        public readonly int[] increments;
        public int incrementIndex;
        private bool hasResetSeedToPreviousGood = true;

        public bool IncrementSeed(ref int currentSeed)
        {
            if (previousSeed == currentSeed || incrementIndex < increments.Length - 1)
            {
                incrementIndex = previousSeed == currentSeed ? 0 : incrementIndex + 1;
                currentSeed = previousSeed + increments[incrementIndex];
                Debug.Log($"Incrementing by {increments[incrementIndex]}");
                FileStuff.shouldLog = previousSeed == currentSeed;
                return true;
            }
            
            if (!hasResetSeedToPreviousGood)
            {
                currentSeed = previousSeed;
                hasResetSeedToPreviousGood = true;
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