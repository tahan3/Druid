using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "ItemsDataBundle", menuName = "ItemsDataBundle", order = 0)]
    public class ItemsDataBundle : ScriptableObject
    {
        public List<ItemConfig> itemConfigs;
    }
}