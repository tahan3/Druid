using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Source.Scripts.Items;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "ItemsStorage", menuName = "ItemsStorage", order = 0)]
    public class GlobalItemsData : ScriptableObjectInstaller
    {
        [SerializeField] private List<ItemsDataBundle> itemsDataBundles;

        private Dictionary<ItemType, Dictionary<string, ItemConfig>> _items;
        
        public override void InstallBindings()
        {
            InitializeData();

            Container.Bind<GlobalItemsData>().FromInstance(this).AsSingle().NonLazy();
        }

        private void InitializeData()
        {
            _items = new Dictionary<ItemType, Dictionary<string, ItemConfig>>();

            foreach (var itemsDataBundle in itemsDataBundles)
            {
                foreach (var itemConfig in itemsDataBundle.itemConfigs)
                {
                    TryAddItem(itemConfig);
                }
            }
        }
        
        private bool TryAddItem(ItemConfig item)
        {
            if (!_items.ContainsKey(item.item.type))
            {
                _items.Add(item.item.type, new Dictionary<string, ItemConfig>());
            }

            if (_items[item.item.type].ContainsKey(item.item.name))
            {
                return false;
            }
            
            _items[item.item.type].Add(item.item.name, item);

            return true;
        }

        public ItemConfig GetRandomItem()
        {
            var type = _items.Keys.ToList()[Random.Range(0, _items.Keys.Count)];
            return _items[type].Values.ToList()[Random.Range(0, _items[type].Values.Count)];
        }
        
        public bool TryGetSprite(ItemType itemType, string itemName, out Sprite sprite)
        {
            sprite = null;
            
            if (TryGetItemConfig(itemType, itemName, out var config))
            {
                sprite = config.sprite;
                return true;
            }

            return false;
        }
        
        public bool TryGetItemData(ItemType itemType, string itemName, out Item item)
        {
            item = null;
            
            if (TryGetItemConfig(itemType, itemName, out var config))
            {
                item = config.item;
                return true;
            }

            return false;
        }
        
        private bool TryGetItemConfig(ItemType itemType, string itemName, out ItemConfig itemConfig)
        {
            itemConfig = null;
            
            if (_items.TryGetValue(itemType, out var item))
            {
                if (item.TryGetValue(itemName, out var config) && config != null)
                {
                    itemConfig = config;
                    return true;
                }
            }

            return false;
        }
    }
}