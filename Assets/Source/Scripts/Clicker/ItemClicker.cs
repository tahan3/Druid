using System;
using Source.Scripts.Inventory;
using Source.Scripts.Items;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Source.Scripts.Clicker
{
    public class ItemClicker : IClicker<Item>, ITickable
    {
        public event Action<Item> OnClick;
        
        private Camera _camera = Camera.main;
        private readonly int _layerMask = 1 << LayerMask.NameToLayer("Item");

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast( /*_camera*/_camera.ScreenToWorldPoint(Input.mousePosition),
                    Vector2.zero, float.PositiveInfinity, _layerMask);

                if (hit.collider != null)
                {
                    OnClick?.Invoke(hit.collider.GetComponent<ItemView>().item);
                    Object.Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}