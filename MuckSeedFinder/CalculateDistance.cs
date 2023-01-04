using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class CalculateDistance
    {
        public static double CalculateShortestDistance(Vector3 spawn, List<Vector3> chiefsSpears, List<Vector3> guardians, Vector3 boat)
        {
            double shortestDistance = double.MaxValue;

            foreach (Vector3 weapon in chiefsSpears)
            {
                foreach (Vector3 village in chiefsSpears)
                {
                    List<Vector3> travelPoints = new List<Vector3>()
                    {
                        spawn,
                        weapon,
                        guardians[0], guardians[1], guardians[2], guardians[3], guardians[4],
                        village,
                        boat
                    };

                    shortestDistance = Math.Min(shortestDistance, CalculateShortestDistance(travelPoints, 2, 6));
                }
            }

            return shortestDistance;
        }

        // First is the first index of list to permute and last is the last index of list to permute
        private static double CalculateShortestDistance(List<Vector3> travelPoints, int first, int last)
        {
            double shortestDistance = double.MaxValue;
            CalculateShortestDistance(travelPoints, first, last, ref shortestDistance);
            return shortestDistance;
        }

        // Calculates the shortest distance by testing all possible permuations
        private static void CalculateShortestDistance(List<Vector3> travelPoints, int first, int last, ref double shortestDistance)
        {
            if (first == last)
            {
                shortestDistance = Math.Min(shortestDistance, CalculateMultipleDistances(travelPoints));
                return;
            }

            for (int i = first; i <= last; i++)
            {
                (travelPoints[first], travelPoints[i]) = (travelPoints[i], travelPoints[first]);
                CalculateShortestDistance(travelPoints, first + 1, last, ref shortestDistance);
                (travelPoints[first], travelPoints[i]) = (travelPoints[i], travelPoints[first]);
            }
        }

        private static double CalculateMultipleDistances(List<Vector3> points)
        {
            double totalDistance = 0;

            for (int i = 0; i < points.Count-1; i++)
            {
                totalDistance += Vector3.Distance(points[i], points[i + 1]);
            }

            return totalDistance;
        }
    }
}
