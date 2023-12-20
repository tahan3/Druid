using System;
using UnityEngine.UI;

namespace Source.Scripts.Views.Inventory
{
    public class InventoryButton<T> where T : Enum
    {
        public T Type;
        public Button Button;
    }
}