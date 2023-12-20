using System;

namespace Source.Scripts.Clicker
{
    public interface IClicker<out T>
    {
        public event Action<T> OnClick;
    }
}