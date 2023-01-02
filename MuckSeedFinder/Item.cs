using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class Item
    {
        public string name;
        public Vector3 position;
        public Item(string name, Vector3 position)
        {
            this.name = name;
            this.position = position;
        }
    }
}
