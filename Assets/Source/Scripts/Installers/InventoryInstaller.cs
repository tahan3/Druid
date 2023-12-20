using System;
using Source.Scripts.Clicker;
using Source.Scripts.Inventory;
using Source.Scripts.Views;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform viewsParent;

        private ViewsHandler _viewsHandler;
        private ItemClicker _itemClicker;
        private InventorySlotsStorage _inventorySlotsStorage;
        private InventoryEventsHandler _inventoryEventsHandler;
        
        public override void InstallBindings()
        {
            _itemClicker = new ItemClicker();
            Container.BindInterfacesTo<ItemClicker>().FromInstance(_itemClicker).AsSingle().NonLazy();

            Container.Bind<IViewsHandler>().To<ViewsHandler>().FromNew().AsSingle()
                .WithArguments(viewsParent)
                .NonLazy();

            _inventoryEventsHandler = new InventoryEventsHandler();
            _inventorySlotsStorage = new InventorySlotsStorage();
            _itemClicker.OnClick += (x) => _inventoryEventsHandler.OnGetItem?.Invoke(x);
            _itemClicker.OnClick += _inventorySlotsStorage.Add;
            
            Container.Bind<IInventorySlotsStorage>().To<InventorySlotsStorage>().FromInstance(_inventorySlotsStorage).AsSingle().NonLazy();
            Container.Bind<InventoryEventsHandler>().ToSelf().FromInstance(_inventoryEventsHandler).AsSingle();
            Container.Bind<InventoryHandler>().ToSelf().FromNew().AsSingle().NonLazy();
        }
    }
}