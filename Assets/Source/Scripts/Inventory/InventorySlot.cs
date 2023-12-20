using System;
using Source.Scripts.Data;
using Source.Scripts.Items;
using UnityEngine.Serialization;

namespace Source.Scripts.Inventory
{
    [Serializable]
    public class InventorySlot
    {
        public Item item;
        public int count;

        public InventorySlot(Item item, int count = 1)
        {
            this.item = item;
            this.count = count;
        }
    }
}