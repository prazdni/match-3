using System;
using Animation;
using Enum;
using Interface;
using Model;
using Pull;

namespace ViewModel
{
    public sealed class SearcherViewModel : IViewModel
    {
        public event Action OnAction = () => { };

        public bool DidFind { get; private set; }

        public bool IsAnimating => _slideAnimation.IsAnimating;

        private readonly SlideAnimation _slideAnimation;
        private readonly TableCleaner _tableCleaner;
        private readonly RestructureChips _restructure;
        
        private readonly ChipModel[,] _chips;
        private bool _isClicked;

        public SearcherViewModel(ChipPull chipPull, ScoreViewModel scoreViewModel, 
            IViewModel clickViewModel, IAnimationSequences<ChipModel> animationSequences)
        {
            _chips = chipPull.Chips;
            _slideAnimation = new SlideAnimation(animationSequences);
            _tableCleaner = new TableCleaner(chipPull, scoreViewModel);
            _restructure = new RestructureChips();
            
            clickViewModel.OnAction += OnClick;
            
            StartSearching();
        }
        
        public void Action()
        {
            StartSearching();
            OnAction.Invoke();
        }

        private void StartSearching()
        {
            var isHorizontalDone = HorizontalSearch();
                
            var isVerticalDone = VerticalSearch();

            DidFind = isHorizontalDone || isVerticalDone;

            if (DidFind)
            {
                _restructure.Restructure(_chips);

                if (_isClicked)
                {
                    _slideAnimation.StartAnimation(_chips);
                }

                _tableCleaner.CleanTable(_chips);
            }
        }

        private bool HorizontalSearch()
        {
            ChipType type = ChipType.None;

            var isSomeoneMarked = false;
            
            for (int x = 0; x < _chips.GetLength(0); x++)
            {
                int counter = 0;
                
                for (int y = 0; y < _chips.GetLength(1); y++)
                {
                    if (counter == 0)
                    {
                        type = _chips[x, y].ChipType;
                        counter++;
                    }
                    else
                    {
                        if (type == _chips[x, y].ChipType)
                        {
                            counter++;
                        }
                        else
                        {
                            if (counter > 2)
                            {
                                MarkElements(x, y - 1, counter, true);
                                isSomeoneMarked = true;
                                type = _chips[x, y].ChipType;
                                counter = 1;
                            }
                            else
                            {
                                type = _chips[x, y].ChipType;
                                counter = 1;
                            }
                        }
                    }
                }

                if (counter > 2)
                {
                    MarkElements(x, _chips.GetLength(1) - 1, counter, true);
                    isSomeoneMarked = true;
                }
            }

            return isSomeoneMarked;
        }

        private bool VerticalSearch()
        {
            ChipType type = ChipType.None;
            
            var isSomeoneMarked = false;
            
            for (int y = 0; y < _chips.GetLength(1); y++)
            {
                int counter = 0;
                
                for (int x = 0; x < _chips.GetLength(0); x++)
                {
                    if (counter == 0)
                    {
                        type = _chips[x, y].ChipType;
                        counter++;
                    }
                    else
                    {
                        if (type == _chips[x, y].ChipType)
                        {
                            counter++;
                        }
                        else
                        {
                            if (counter > 2)
                            {
                                MarkElements(x - 1, y, counter, false);
                                isSomeoneMarked = true;
                                type = _chips[x, y].ChipType;
                                counter = 1;
                            }
                            else
                            {
                                type = _chips[x, y].ChipType;
                                counter = 1;   
                            }
                        }
                    }
                }

                if (counter > 2)
                {
                    MarkElements(_chips.GetLength(0) - 1, y, counter, false);
                    isSomeoneMarked = true;
                }
            }

            return isSomeoneMarked;
        }
        
        private void MarkElements(int i, int j, int counter, bool isHorizontal)
        {
            if (isHorizontal)
            {
                var x = i;
                for (int y = j; y > j - counter; y--)
                {
                    _chips[x, y].Marker = true;
                }
            }
            else
            {
                var y = j;
                for (int x = i; x > i - counter; x--)
                {
                    _chips[x, y].Marker = true;
                }
            }
        }

        private void OnClick()
        {
            _isClicked = true;
        }
    }
}