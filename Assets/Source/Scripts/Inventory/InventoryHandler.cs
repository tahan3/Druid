using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Source.Scripts.Characteristics;
using Source.Scripts.Data;
using Source.Scripts.Items;
using UnityEngine;

namespace Source.Scripts.Inventory
{
    public class InventoryHandler
    {
        private readonly IInventorySlotsStorage _inventorySlotsStorage;
        private readonly GlobalItemsData _globalItemsData;

        public InventoryHandler(IInventorySlotsStorage inventorySlotsStorage, GlobalItemsData globalItemsData)
        {
            _inventorySlotsStorage = inventorySlotsStorage;
            _globalItemsData = globalItemsData;
        }

        public List<InventorySlot> GetInventorySlots()
        {
            List<InventorySlot> slots = new List<InventorySlot>();

            foreach (var inventoryKey in _inventorySlotsStorage.Inventory.Keys)
            {
                slots.AddRange(GetInventorySlots(inventoryKey));
            }

            return slots;
        }
        
        public Sprite GetItemSprite(Item item)
        {
            _globalItemsData.TryGetSprite(item.type, item.name, out var sprite);
            return sprite;
        }
        
        public List<InventorySlot> GetInventorySlots(ItemType type)
        {
            return _inventorySlotsStorage.Inventory[type].Values.ToList();
        }

        public List<InventorySlot> GetInventorySlots(CharacteristicType characteristicType)
        {
            List<InventorySlot> slots = new List<InventorySlot>();

            foreach (var inventoryKey in _inventorySlotsStorage.Inventory.Keys)
            {
                slots.AddRange(_inventorySlotsStorage.Inventory[inventoryKey].Values
                    .Where(x => x.item.characteristics
                        .Exists(y => y.type == characteristicType)).ToList());
            }
            
            return slots;
        }
        
        public List<InventorySlot> GetInventorySlots(ItemType itemType, CharacteristicType characteristicType)
        {
            return _inventorySlotsStorage.Inventory[itemType].Values
                .Where(x => x.item.characteristics.Exists(y => y.type == characteristicType)).ToList();
        }
    }
}