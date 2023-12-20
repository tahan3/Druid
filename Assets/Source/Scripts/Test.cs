using System.Collections;
using Source.Scripts.Data;
using Source.Scripts.Items;
using UnityEngine;
using Zenject;

namespace Source.Scripts
{
    public class Test : MonoInstaller
    {
        [SerializeField] private Vector2 upperSpawnBounds;
        [SerializeField] private Vector2 lowerSpawnBounds;
        [SerializeField] private float spawnDelay = 1f;
        [SerializeField] private ItemView prefab;
        
        private RandomItemSpawner _randomItemSpawner;

        public override void InstallBindings()
        {
            Container.Bind<ItemView>().FromInstance(prefab).AsSingle();

            _randomItemSpawner = Container.Instantiate<RandomItemSpawner>();

            StartCoroutine(SpawnTest());
        }

        private IEnumerator SpawnTest()
        {
            while (gameObject.activeSelf)
            {
                var item = _randomItemSpawner.Spawn();
                
                item.transform.position = new Vector2(
                    Random.Range(lowerSpawnBounds.x, upperSpawnBounds.x),
                    Random.Range(lowerSpawnBounds.y, upperSpawnBounds.y));
                
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
