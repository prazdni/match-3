using System;
using Interface;
using UnityEngine;

namespace ViewModel
{
    public sealed class ButtonPressedViewModel : IViewModel<KeyCode>
    {
        public event Action<KeyCode> OnAction = (t) => { };

        public void Action(KeyCode obj)
        {
            OnAction.Invoke(obj);
        }
    }
}