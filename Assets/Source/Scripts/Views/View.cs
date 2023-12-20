using UnityEngine;
using Zenject;

namespace Source.Scripts.Views
{
    public abstract class View : MonoBehaviour, IView
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}