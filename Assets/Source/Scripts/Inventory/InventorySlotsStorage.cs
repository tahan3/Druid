using System.Collections.Generic;
using Source.Scripts.Items;
using Source.Scripts.SaveLoad;

namespace Source.Scripts.Inventory
{
    public class InventorySlotsStorage : IInventorySlotsStorage
    {
        public Dictionary<ItemType, Dictionary<string, InventorySlot>> Inventory { get; private set; }

        public InventorySlotsStorage()
        {
            LoadData();
        }
        
        public void LoadData()
        {
            //load from binary
            //dictionary deserialization
            Inventory = new Dictionary<ItemType, Dictionary<string, InventorySlot>>();
        }
        
        public void SaveData()
        {
            //dictionary serialization
            //save to binary
        }

        public void Add(Item data)
        {
            if (!Inventory.ContainsKey(data.type))
            {
                Inventory.Add(data.type, new Dictionary<string, InventorySlot>());
            }

            if (Inventory[data.type].ContainsKey(data.name))
            {
                Inventory[data.type][data.name].count++;
            }
            else
            {
                Inventory[data.type].Add(data.name, new InventorySlot(data));
            }
        }

        public void Remove(Item data)
        {
            if (!Inventory.ContainsKey(data.type))
            {
                if (Inventory[data.type].ContainsKey(data.name))
                {
                    var slot = Inventory[data.type][data.name];
                    
                    if (slot.count > 0)
                    {
                        slot.count--;
                    }
                    else
                    {
                        Inventory[data.type].Remove(slot.item.name);
                    }
                }
            }
        }
    }
}