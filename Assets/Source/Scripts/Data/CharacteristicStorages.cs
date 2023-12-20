using Source.Scripts.Characteristics;
using UnityEngine;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "CharacteristicSpritesStorage", menuName = "CharacteristicSpritesStorage", order = 0)]
    public class CharacteristicStorages : StorageByType<CharacteristicType,Sprite>
    {
    }
}