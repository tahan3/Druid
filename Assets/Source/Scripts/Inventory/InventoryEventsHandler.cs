using System;
using Source.Scripts.Items;

namespace Source.Scripts.Inventory
{
    public class InventoryEventsHandler
    {
        public Action<Item> OnGetItem;
    }
}