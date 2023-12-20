using System.Collections.Generic;
using Source.Scripts.Characteristics;
using Source.Scripts.Data;
using Source.Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Views.Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private RectTransform characteristicsContainer;
        [SerializeField] private CharacteristicSlotView characteristicPrefab;

        private const string CountPrefix = "X";

        private List<CharacteristicSlotView> _slots = new List<CharacteristicSlotView>();

        [Inject] private StorageByType<CharacteristicType, Sprite> _characteristicTypeSprites;
        
        public void Init(Sprite sprite, int count, List<Characteristic> characteristics)
        {
            icon.sprite = sprite;
            countText.text = CountPrefix + count;

            InitSlots(characteristics);
        }

        private void InitSlots(List<Characteristic> characteristics)
        {
            DisableSlots();
            
            for (var i = 0; i < characteristics.Count; i++)
            {
                if (i >= _slots.Count)
                {
                    CreateSlot();
                }

                _slots[i].Init(_characteristicTypeSprites.GetValue(characteristics[i].type), characteristics[i].count);
                _slots[i].gameObject.SetActive(true);
            }
        }

        private void DisableSlots()
        {
            _slots.ForEach(x => x.gameObject.SetActive(false));
        }
        
        private CharacteristicSlotView CreateSlot()
        {
            var slot = Instantiate(characteristicPrefab, characteristicsContainer);
            _slots.Add(slot);

            return slot;
        }
    }
}