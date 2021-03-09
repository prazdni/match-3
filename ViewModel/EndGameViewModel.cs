using System;
using Interface;

namespace ViewModel
{
    public sealed class EndGameViewModel : IViewModel<bool>, IExecute
    {
        public event Action<bool> OnAction = t => { };
        private int _scoreDifference;
        private float _time;
        private int _step;
        private bool _isHappened;
        public EndGameViewModel(IViewModel<int> stepViewModel, IViewModel<int,int> scoreViewModel, 
            IViewModel<float> timerViewModel)
        {
            stepViewModel.OnAction += OnStepChanged;
            scoreViewModel.OnAction += OnScoreChanged;
            timerViewModel.OnAction += OnTimeChanged;

            _isHappened = false;
        }
        
        public void Action(bool obj)
        {
            OnAction.Invoke(obj);
        }

        private void OnTimeChanged(float time)
        {
            _time = time;
        }

        private void OnScoreChanged(int score, int totalScore)
        {
            _scoreDifference = totalScore - score;
        }

        private void OnStepChanged(int step)
        {
            _step = step;
        }

        public void Execute(float deltaTime)
        {
            if (!_isHappened)
            {
                if (_scoreDifference <= 0 && _time > 0 && _step >= 0)
                {
                    OnAction.Invoke(true);
                    _isHappened = true;
                }
                else
                {
                    if (_time < 0 || _step <= 0)
                    {
                        OnAction.Invoke(false);
                        _isHappened = true;
                    }
                }
            }
        }
    }
}