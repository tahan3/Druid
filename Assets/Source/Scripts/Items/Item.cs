using System;
using System.Collections.Generic;
using Source.Scripts.Characteristics;

namespace Source.Scripts.Items
{
    [Serializable]
    public class Item
    {
        public ItemType type;
        public string name;
        public List<Characteristic> characteristics;
    }
}