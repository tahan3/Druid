using AYellowpaper.SerializedCollections;
using Source.Scripts.Views;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "ViewData", menuName = "ViewData", order = 0)]
    public class ViewData : StorageByType<ViewType, View>
    {
    }
}