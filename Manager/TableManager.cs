using Interface;
using ViewModel;

namespace Manager
{
    public sealed class TableManager : IExecute
    {
        private readonly HighlightChipViewModel _highlight;
        private readonly SearcherViewModel _tableViewModel;
        private readonly IViewModel _shuffleViewModel;
        private readonly TimerViewModel _timerViewModel;
        private readonly IExecute _endGameExecute;

        private bool _isFirstLoad;
        private bool _hasHighlightCheck;

        public TableManager(SearcherViewModel tableViewModel, HighlightChipViewModel highlight,
            TimerViewModel timerViewModel, IExecute endGameExecute, IViewModel shuffleViewModel)
        {
            _tableViewModel = tableViewModel;
            _highlight = highlight;
            _shuffleViewModel = shuffleViewModel;
            _timerViewModel = timerViewModel;
            _endGameExecute = endGameExecute;
        }

        public void Execute(float deltaTime)
        {
            if (!_tableViewModel.IsAnimating)
            {
                if (_tableViewModel.DidFind)
                {
                    _tableViewModel.Action();
                    _hasHighlightCheck = false;
                }
                else
                {
                    if (!_hasHighlightCheck)
                    {
                        _highlight.Action(false);
                        _hasHighlightCheck = true;
                    }

                    if (!_highlight.HasHighlight)
                    {
                        _shuffleViewModel.Action();
                    }
                }
            }

            _timerViewModel.Execute(deltaTime);
            _endGameExecute.Execute(deltaTime);
        }
    }
}