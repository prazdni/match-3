using System;
using Interface;
using Manager;

namespace ViewModel
{
    public sealed class TimerViewModel : IExecute, IViewModel<float>
    {
        public event Action<float> OnAction = (t) => { };
        
        private readonly IViewModel<bool> _highlightChip;
        private readonly SearcherViewModel _searcherViewModel;


        private readonly Timer _totalTime;
        private readonly Timer _highlightTime;
        private bool _isHighlighted;

        public TimerViewModel(float maxTime, float highlightTime, IViewModel<bool> highlightChip, 
            IViewModel replaceViewModel, SearcherViewModel searcherViewModel)
        {
            _highlightChip = highlightChip;
            _searcherViewModel = searcherViewModel;
            replaceViewModel.OnAction += OnClick;
            
            _totalTime = new Timer(0, maxTime);
            _highlightTime = new Timer(0, highlightTime);
        }
        
        public void Execute(float deltaTime)
        {
            OnAnimation();
            
            _totalTime.TimerTick(deltaTime);
            
            OnAction.Invoke(_totalTime.CurrentTime);
            
            Highlight(deltaTime);
        }

        private void Highlight(float deltaTime)
        {
            if (_highlightTime.CurrentTime > _highlightTime.TimerMinValue)
            {
                _highlightTime.TimerTick(deltaTime);
            }
            else
            {
                if (!_isHighlighted)
                {
                    _highlightChip.Action(true);
                    _isHighlighted = true;
                }
            }
        }

        private void OnClick()
        {
            _highlightTime.ResetTimeCount();
            _isHighlighted = false;
        }

        private void OnAnimation()
        {
            if (_searcherViewModel.IsAnimating)
            {
                _highlightTime.ResetTimeCount();
                _isHighlighted = false;
            }
        }

        public void Action(float obj)
        {
            OnAction.Invoke(obj);
        }
    }
}