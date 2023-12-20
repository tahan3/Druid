using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Views.MainMenu
{
    public class MainMenu : View
    {
        [SerializeField] private Button showInventoryButton;

        [Inject] private IViewsHandler _viewsHandler;

        private void Start()
        {
            showInventoryButton.onClick.AddListener(() => _viewsHandler.ShowView(ViewType.Inventory));
        }
    }
}