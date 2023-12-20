using System.Collections.Generic;
using Source.Scripts.Container;
using Source.Scripts.Data;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Views
{
    public sealed class ViewsHandler : IViewsHandler
    {
        [Inject] private StorageByType<ViewType, View> _viewPrefabs;
        [Inject] private DiContainer _container;

        private Dictionary<ViewType, View> _views;

        private View _currentView;

        private RectTransform _parent;
        
        public ViewsHandler(RectTransform parent)
        {
            _views = new Dictionary<ViewType, View>();
            _parent = parent;
        }

        [Inject]
        private void Init(DiContainer container)
        {
            _container = container;
            ShowView(ViewType.MainMenu);
        }
        
        public View ShowView(ViewType type)
        {
            if (!_views.ContainsKey(type))
            {
                var view = _container.InstantiatePrefab(_viewPrefabs.GetValue(type), _parent);
                _views.Add(type, view.GetComponent<View>());
            }

            if (_currentView != null)
            {
                _currentView.Close();
            }
            
            _currentView = _views[type];
            _currentView.Open();

            return _currentView;
        }
    }
}