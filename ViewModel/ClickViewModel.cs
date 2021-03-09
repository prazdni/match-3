using System;
using Interface;

namespace ViewModel
{
    public sealed class ClickViewModel : IViewModel
    {
        public event Action OnAction = () => { };

        public void Action()
        {
            OnAction.Invoke();
        }
    }
}