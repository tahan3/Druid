using System.Collections.Generic;
using Source.Scripts.Items;
using Source.Scripts.SaveLoad;

namespace Source.Scripts.Inventory
{
    public interface IInventorySlotsStorage : IStorage<Item>
    {
        public Dictionary<ItemType, Dictionary<string, InventorySlot>> Inventory { get; }
    }
}