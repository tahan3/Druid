using System;
using System.Collections.Generic;
using Source.Scripts.Views.Inventory;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.UI
{
    public class ComponentsKeyValueContainer<TKey, TValue> where TValue : Component
    {
        private Transform _parent;
        private TValue _prefab;

        private Dictionary<TKey, TValue> _values;

        public ComponentsKeyValueContainer(Transform parent, TValue prefab)
        {
            _parent = parent;
            _prefab = prefab;
            _values = new Dictionary<TKey, TValue>();
        }

        public TValue AddValue(TKey key)
        {
            if (KeyCheck(key)) return null;
            
            var obj = Object.Instantiate(_prefab, _parent);
            _values.Add(key, obj);

            return obj;
        }

        public bool AddValue(TKey key, TValue instance)
        {
            if (KeyCheck(key)) return false;

            _values.Add(key, instance);

            return true;
        }
        
        public bool KeyCheck(TKey key)
        {
            return _values.ContainsKey(key);
        }

        public bool ValueCheck(TValue value)
        {
            return _values.ContainsValue(value);
        }
    }
}