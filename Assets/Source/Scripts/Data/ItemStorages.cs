using Source.Scripts.Items;
using UnityEngine;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "ItemSpritesStorage", menuName = "ItemSpritesStorage", order = 0)]
    public class ItemStorages : StorageByType<ItemType, Sprite>
    {
    }
}