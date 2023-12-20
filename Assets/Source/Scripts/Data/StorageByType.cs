using AYellowpaper.SerializedCollections;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Data
{
    public abstract class StorageByType<K,V> : ScriptableObjectInstaller
    {
        [SerializedDictionary("Key", "Value")]
        [SerializeField] protected SerializedDictionary<K, V> items;

        public virtual V GetValue(K type)
        {
            return items[type];
        }

        public override void InstallBindings()
        {
            Container.Bind<StorageByType<K, V>>().FromInstance(this).AsSingle();
        }
    }
}