using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Characteristics
{
    [Serializable]
    public struct Characteristic
    {
        public CharacteristicType type;
        public int count;
    }
}