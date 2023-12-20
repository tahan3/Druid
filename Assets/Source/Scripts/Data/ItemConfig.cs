using System;
using System.Collections.Generic;
using Source.Scripts.Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "ItemConfig", order = 0)]
    public class ItemConfig : ScriptableObject
    {
        public Item item;
        public Sprite sprite;
    }
}