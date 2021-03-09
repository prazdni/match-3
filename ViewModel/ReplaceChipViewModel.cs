using System;
using Animation;
using DG.Tweening;
using Interface;
using Model;
using Pull;
using UnityEngine;

namespace ViewModel
{
    public sealed class ReplaceChipViewModel : IViewModel<Transform>
    {
        public event Action<Transform> OnAction = (t) => { };

        private readonly IViewModel _clickViewModel;
        private readonly ChipPull _chipPull;
        private readonly SearcherViewModel _searcher;
        private readonly IViewModel<int> _stepViewModel;
        private readonly ChipModel[] _chips;
        private readonly ITweenAnimation<ChipModel[]> _tweenAnimation;
        private Sequence _sequence;

        private int _counter;

        public ReplaceChipViewModel(ChipPull chipPull, SearcherViewModel searcher, IViewModel clickViewModel, 
            IViewModel<int> stepViewModel, ITweenAnimation<ChipModel[]> tweenAnimation)
        {
            _chipPull = chipPull;
            _searcher = searcher;
            _clickViewModel = clickViewModel;
            _stepViewModel = stepViewModel;

            _chips = new ChipModel[2];

            _counter = 0;

            _tweenAnimation = tweenAnimation;
        }

        public void Action(Transform obj)
        {
            OnClick(obj);
            OnAction.Invoke(obj);
        }

        private void OnClick(Transform transform)
        {
            var chip = _chipPull.Get(transform);
            _chips[_counter] = chip;

            _counter++;
            
            if (_counter > 1)
            {
                _counter = 0;
                
                _tweenAnimation.StopAnimation(_chips);

                var allChips = _chipPull.Chips;
                var newPull = _chipPull as IPull<Transform, Vector2Int>;

                var index1 = newPull.Get(_chips[0].Transform);
                var index2 = newPull.Get(_chips[1].Transform);
                
                if ((Mathf.Abs(index1.x - index2.x) == 1 && Mathf.Abs(index1.y - index2.y) == 0) ||
                    (Mathf.Abs(index1.x - index2.x) == 0 && Mathf.Abs(index1.y - index2.y) == 1))
                {
                    ReplaceChip(allChips, index1, index2);

                    var doesMatch = CheckChip(allChips, index1);

                    if (!doesMatch)
                    {
                        doesMatch = CheckChip(allChips, index2);
                    }
                    
                    if (!doesMatch)
                    {
                        index1 = newPull.Get(_chips[0].Transform);
                        index2 = newPull.Get(_chips[1].Transform);
                        ReplaceChip(allChips, index1, index2);
                    }
                    else
                    {
                        _stepViewModel.Action(1);
                        _searcher.Action();
                    }
                }
            }
            else
            {
                _tweenAnimation.StartAnimation(_chips);
            }

            _clickViewModel.Action();
        }

        private bool CheckChip(ChipModel[,] chips, Vector2Int index)
        {
            var doesMatch = false;
            
            for (int i = 0; i < 3; i++)
            {
                if (!doesMatch)
                {
                    doesMatch = MatchCheck(chips, new Vector2Int(index.x + i, index.y), 
                        new Vector2Int(index.x + i - 1, index.y),
                        new Vector2Int(index.x + i - 2, index.y));
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (!doesMatch)
                {
                    doesMatch = MatchCheck(chips, new Vector2Int(index.x, index.y + i), 
                        new Vector2Int(index.x, index.y + i - 1),
                        new Vector2Int(index.x, index.y + i - 2));
                }
            }

            return doesMatch;
        }

        private bool MatchCheck(ChipModel[,] chips, Vector2Int index1, Vector2Int index2, Vector2Int index3)
        {
            var doesMatch = false;
            
            if (index1.x >= 0 && index1.x < chips.GetLength(0) && 
                index2.x >= 0 && index2.x < chips.GetLength(0) &&
                index3.x >= 0 && index3.x < chips.GetLength(0) &&
                index1.y >= 0 && index1.y < chips.GetLength(1) && 
                index2.y >= 0 && index2.y < chips.GetLength(1) &&
                index3.y >= 0 && index3.y < chips.GetLength(1))
            {
                doesMatch = chips[index1.x, index1.y].ChipType == chips[index2.x, index2.y].ChipType &&
                            chips[index2.x, index2.y].ChipType == chips[index3.x, index3.y].ChipType;
            }

            return doesMatch;
        }

        private void ReplaceChip(ChipModel[,] chips, Vector2Int index1, Vector2Int index2)
        {
            var pos = _chips[0].Transform.position;
            _chips[0].Transform.position = _chips[1].Transform.position;
            _chips[1].Transform.position = pos;

            chips[index1.x, index1.y] = _chips[1];
            chips[index2.x, index2.y] = _chips[0];
        }
    }
}