using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.Inventory
{
    public class CharacteristicSlotView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI countText;

        public void Init(Sprite sprite, int count)
        {
            icon.sprite = sprite;
            countText.text = "+" + count;
        }
    }
}