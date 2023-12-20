using System.Collections.Generic;
using Source.Scripts.Characteristics;
using Source.Scripts.Container;
using Source.Scripts.Data;
using Source.Scripts.Inventory;
using Source.Scripts.Items;
using UnityEngine;
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
        
        private InventoryHandler _inventoryHandler;
        private IViewsHandler _viewsHandler;
        private StorageByType<ItemType, Sprite> _itemTypeSprites;
        private StorageByType<CharacteristicType, Sprite> _characteristicTypeSprites;

        private List<InventoryButton<ItemType>> _itemSortButtons;
        private List<InventoryButton<CharacteristicType>> _characteristicSortButtons;

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
            _itemSortButtons = new List<InventoryButton<ItemType>>();
            _characteristicSortButtons = new List<InventoryButton<CharacteristicType>>();

            _itemTypeSprites = itemTypeSprites;
            _characteristicTypeSprites = characteristicTypeSprites;

            eventsHandler.OnGetItem += NewButtonCheck;

            mainMenuButton.onClick.AddListener(() => _viewsHandler.ShowView(ViewType.MainMenu));
                
            /*InitSlots();*/
        }

        private void InitSlots()
        {
            var inventory = _inventoryHandler.GetInventorySlots();
            
            foreach (var inventorySlot in inventory)
            {
                var slot = CreateSlot();
                slot.Init(_inventoryHandler.GetItemSprite(inventorySlot.item), inventorySlot.count,
                    inventorySlot.item.characteristics);
                slot.gameObject.SetActive(true);

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
            ShowSlots(_inventoryHandler.GetInventorySlots(_currentItemType, characteristicType));
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
            if (!_characteristicSortButtons.Exists(x => x.Type == characteristicType))
            {
                var button = Instantiate(characteristicButtonPrefab, characteristicButtonsContainer);
                button.image.sprite = _characteristicTypeSprites.GetValue(characteristicType);
                _characteristicSortButtons.Add(new InventoryButton<CharacteristicType>()
                    { Button = button, Type = characteristicType });

                button.onClick.AddListener(() => ShowInventory(characteristicType));
            }
        }

        private void ItemTypeButtonCheck(ItemType itemType)
        {
            if (!_itemSortButtons.Exists(x => x.Type == itemType))
            {
                var button = Instantiate(itemTypeButtonPrefab, itemTypeButtonsContainer);
                button.image.sprite = _itemTypeSprites.GetValue(itemType);
                _itemSortButtons.Add(new InventoryButton<ItemType>() { Button = button, Type = itemType });

                button.onClick.AddListener(() => ShowInventory(itemType));
            }
        }
    }
}