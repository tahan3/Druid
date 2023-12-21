using System;
using System.Collections.Generic;
using Source.Scripts.Characteristics;
using Source.Scripts.Container;
using Source.Scripts.Data;
using Source.Scripts.Inventory;
using Source.Scripts.Items;
using Source.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Views.Inventory
{
    public class InventoryView : View
    {
        [Header("Containers")]
        [SerializeField] private RectTransform slotsContainer;
        [SerializeField] private RectTransform characteristicButtonsContainer;
        [SerializeField] private RectTransform itemTypeButtonsContainer;
        
        [Header("Prefabs")]
        [SerializeField] private Button characteristicButtonPrefab;
        [SerializeField] private Button itemTypeButtonPrefab;
        [SerializeField] private InventorySlotView slotPrefab;

        [Header("Buttons")] 
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button allButton;
        
        private InventoryHandler _inventoryHandler;
        private IViewsHandler _viewsHandler;
        private StorageByType<ItemType, Sprite> _itemTypeSprites;
        private StorageByType<CharacteristicType, Sprite> _characteristicTypeSprites;

        private ComponentsKeyValueContainer<ItemType, Button> _itemSortButtons;
        private ComponentsKeyValueContainer<CharacteristicType, Button> _characteristicSortButtons;

        private List<InventorySlotView> _slots;
        
        private ItemType _currentItemType;
        private CharacteristicType _currentCharacteristicType;

        [Inject]
        private void Construct(InventoryHandler inventoryHandler, InventoryEventsHandler eventsHandler,
            StorageByType<ItemType, Sprite> itemTypeSprites,
            StorageByType<CharacteristicType, Sprite> characteristicTypeSprites,
            IViewsHandler viewsHandler)
        {
            _inventoryHandler = inventoryHandler;
            _viewsHandler = viewsHandler;

            _slots = new List<InventorySlotView>();
            _itemSortButtons =
                new ComponentsKeyValueContainer<ItemType, Button>(itemTypeButtonsContainer, itemTypeButtonPrefab);
            _characteristicSortButtons =
                new ComponentsKeyValueContainer<CharacteristicType, Button>(characteristicButtonsContainer,
                    characteristicButtonPrefab);

            _itemTypeSprites = itemTypeSprites;
            _characteristicTypeSprites = characteristicTypeSprites;

            eventsHandler.OnGetItem += NewButtonCheck;

            mainMenuButton.onClick.AddListener(() => _viewsHandler.ShowView(ViewType.MainMenu));
            allButton.onClick.AddListener(ShowInventory);
                
            InitSlots();
        }

        private void InitSlots()
        {
            var inventory = _inventoryHandler.GetInventorySlots();
            
            foreach (var inventorySlot in inventory)
            {
                NewButtonCheck(inventorySlot.item);
            }
        }

        public override void Open()
        {
            ShowInventory();
            
            base.Open();
        }

        private void ShowInventory()
        {
            _currentItemType = ItemType.None;
            ShowSlots(_inventoryHandler.GetInventorySlots());
        }
        
        private void ShowInventory(ItemType itemType)
        {
            _currentItemType = itemType;
            ShowSlots(_inventoryHandler.GetInventorySlots(itemType));
        }

        private InventorySlotView CreateSlot()
        {
            var slot = DiContainerRef.Container.InstantiatePrefabForComponent<InventorySlotView>(slotPrefab,
                slotsContainer);
            _slots.Add(slot);

            return slot;
        }
        
        private void ShowInventory(CharacteristicType characteristicType)
        {
            if (_currentItemType == ItemType.None)
            {
                ShowSlots(_inventoryHandler.GetInventorySlots(characteristicType));
            }
            else
            {
                ShowSlots(_inventoryHandler.GetInventorySlots(_currentItemType, characteristicType));
            }
        }

        private void ShowSlots(List<InventorySlot> inventorySlots)
        {
            DisableSlots();
            
            for (var i = 0; i < inventorySlots.Count; i++)
            {
                if (i >= _slots.Count)
                {
                    CreateSlot();
                }

                _slots[i].Init(_inventoryHandler.GetItemSprite(inventorySlots[i].item), inventorySlots[i].count,
                    inventorySlots[i].item.characteristics);
                _slots[i].gameObject.SetActive(true);
            }
        }

        private void DisableSlots()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                _slots[i].gameObject.SetActive(false);
            }
        }
        
        private void NewButtonCheck(Item item)
        {
            foreach (var itemCharacteristic in item.characteristics)
            {
                CharacteristicsButtonCheck(itemCharacteristic.type);
            }

            ItemTypeButtonCheck(item.type);
        }

        private void CharacteristicsButtonCheck(CharacteristicType characteristicType)
        {
            if (!_characteristicSortButtons.KeyCheck(characteristicType))
            {
                InitButton(_characteristicSortButtons.AddValue(characteristicType),
                    _characteristicTypeSprites.GetValue(characteristicType),
                    () => ShowInventory(characteristicType));
            }
        }

        private void ItemTypeButtonCheck(ItemType itemType)
        {
            if (!_itemSortButtons.KeyCheck(itemType))
            {
                InitButton(_itemSortButtons.AddValue(itemType), 
                    _itemTypeSprites.GetValue(itemType),
                    () => ShowInventory(itemType));
            }
        }

        private Button InitButton(Button button, Sprite sprite, UnityAction callback = null)
        {
            button.image.sprite = sprite;
            
            if (callback != null)
            {
                button.onClick.AddListener(callback);
            }

            return button;
        }
    }
}