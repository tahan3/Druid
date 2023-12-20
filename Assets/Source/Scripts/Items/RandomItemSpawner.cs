using Source.Scripts.Data;
using Source.Scripts.Inventory;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Items
{
    public class RandomItemSpawner : ISpawner<ItemView>
    {
        [Inject] private GlobalItemsData _itemsData;
        [Inject] private ItemView _itemPrefab;

        public ItemView Spawn()
        {
            var itemView = Object.Instantiate(_itemPrefab);
            var config = _itemsData.GetRandomItem();
            
            itemView.item = config.item;
            itemView.spriteRenderer.sprite = config.sprite;

            return itemView;
        }
    }
}