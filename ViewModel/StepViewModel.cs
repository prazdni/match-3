using System;
using Interface;

namespace ViewModel
{
    public sealed class StepViewModel : IViewModel<int>
    {
        private int _stepQuantity;
        public event Action<int> OnAction = (t) => { };

        public StepViewModel(int stepQuantity)
        {
            _stepQuantity = stepQuantity;
        }

        public void Action(int obj)
        {
            _stepQuantity -= obj;
            OnAction.Invoke(_stepQuantity);
        }
    }
}