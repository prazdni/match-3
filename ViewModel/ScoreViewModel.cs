using System;
using Interface;

namespace ViewModel
{
    public class ScoreViewModel : IViewModel<int,int>
    {
        public event Action<int,int> OnAction = (t, u) => { };

        private int _totalScore;
        private int _score;
        private bool _isClick;

        public ScoreViewModel(int totalScore, IViewModel clickViewModel)
        {
            _score = 0;
            _totalScore = totalScore;
            clickViewModel.OnAction += OnClick;
        }

        public void Action(int obj1, int obj2)
        {
            if (_isClick)
            {
                _score += obj1;
                _totalScore += obj2;
            }
            
            OnAction.Invoke(_score, _totalScore);
        }

        private void OnClick()
        {
            _isClick = true;
        }
    }
}