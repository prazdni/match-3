using System;
using Animation;
using DG.Tweening;
using Interface;
using Model;
using Pull;
using UnityEngine;

namespace ViewModel
{
    public sealed class HighlightChipViewModel : IViewModel<bool>
    {
        public event Action<bool> OnAction = (t) => { };

        public bool HasHighlight => _hasHighlight;

        private readonly ITweenAnimation<ChipModel[]> _hintTweenAnimation;
        private readonly ChipModel[,] _chips;
        private Sequence _sequence;
        private bool _hasHighlight;
        
        private readonly ChipModel[] _highlightedChips;

        public HighlightChipViewModel(ChipPull chipPull, IViewModel click, ITweenAnimation<ChipModel[]> hintTweenAnimation)
        {
            _hintTweenAnimation = hintTweenAnimation;

            _highlightedChips = new ChipModel[3];
            _chips = chipPull.Chips;
            
            click.OnAction += OnClick;
        }
        
        public void Action(bool obj)
        {
            OnHighlight(obj);
            OnAction.Invoke(obj);
        }

        private void OnHighlight(bool obj)
        {
            _hasHighlight = false;

            for (int x = 0; x < _chips.GetLength(0); x++)
            {
                for (int y = 0; y < _chips.GetLength(1); y++)
                {
                    if (y + 1 < _chips.GetLength(1))
                    {
                        if (_chips[x, y].ChipType == _chips[x, y + 1].ChipType)
                        {
                            HorizontalCheck(new Vector2Int(x, y), new Vector2Int(x, y + 1), 
                                false, false);
                            HorizontalCheck(new Vector2Int(x, y + 1), new Vector2Int(x, y), 
                                true, false);
                        }
                    }

                    if (y + 2 < _chips.GetLength(1))
                    {
                        if (_chips[x, y].ChipType == _chips[x, y + 2].ChipType)
                        {
                            HorizontalCheck(new Vector2Int(x, y), new Vector2Int(x, y + 2), 
                                true, true);
                        }
                    }

                    if (x + 1 < _chips.GetLength(0))
                    {
                        if (_chips[x, y].ChipType == _chips[x + 1, y].ChipType)
                        {
                            VerticalCheck(new Vector2Int(x, y), new Vector2Int(x + 1, y), 
                                false, false);
                            VerticalCheck(new Vector2Int(x + 1, y), new Vector2Int(x, y), 
                                true, false);
                        }
                    }
                    
                    if (x + 2 < _chips.GetLength(0))
                    {
                        if (_chips[x, y].ChipType == _chips[x + 2, y].ChipType)
                        {
                            VerticalCheck(new Vector2Int(x, y), new Vector2Int(x + 2, y), 
                                true, true);
                        }
                    }
                    
                    if (_hasHighlight)
                    {
                        break;
                    }
                }

                if (_hasHighlight)
                {
                    break;
                }
            }

            if (_hasHighlight && obj)
            {
                StartAnimation();
            }
        }

        private void HorizontalCheck(Vector2Int index1, Vector2Int index2, bool isReversed, bool isBetween)
        {
            if (!_hasHighlight)
            {
                for (int i = 0; i < 3; i++)
                {
                    var vec3 = isBetween ? 
                        new Vector2Int(index2.x + i - 1, index2.y - 1) : 
                        new Vector2Int(index2.x + i - 1, isReversed ? index2.y - 1 : index2.y + 1);
                    
                    if (!_hasHighlight)
                    {
                        if (i == 1 && !isBetween)
                        {
                            vec3 = new Vector2Int(index2.x, isReversed ? index2.y - 2 : index2.y + 2);
                        }
                        
                        _hasHighlight = MatchCheck(index1, index2, vec3);
                        
                        if (_hasHighlight)
                        {
                            SetHighlightChips(index1, index2, vec3);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        
        private void VerticalCheck(Vector2Int index1, Vector2Int index2, bool isReversed, bool isBetween)
        {
            if (!_hasHighlight)
            {
                for (int i = 0; i < 3; i++)
                {
                    var vec3 = isBetween ? 
                        new Vector2Int(index2.x - 1, index2.y + i - 1) :
                        new Vector2Int(isReversed ? index2.x - 1 : index2.x + 1, index2.y + i - 1);
                    
                    if (!_hasHighlight)
                    {
                        if (i == 1 && !isBetween)
                        {
                            vec3 = new Vector2Int(isReversed ? index2.x - 2 : index2.x + 2, index2.y);
                        }
                        
                        _hasHighlight = MatchCheck(index1, index2, vec3);
                        
                        if (_hasHighlight)
                        {
                            SetHighlightChips(index1, index2, vec3);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        
        private bool MatchCheck(Vector2Int index1, Vector2Int index2, Vector2Int index3)
        {
            var doesMatch = false;
            
            if (index1.x >= 0 && index1.x < _chips.GetLength(0) && 
                index2.x >= 0 && index2.x < _chips.GetLength(0) &&
                index3.x >= 0 && index3.x < _chips.GetLength(0) &&
                index1.y >= 0 && index1.y < _chips.GetLength(1) && 
                index2.y >= 0 && index2.y < _chips.GetLength(1) &&
                index3.y >= 0 && index3.y < _chips.GetLength(1))
            {
                doesMatch = _chips[index1.x, index1.y].ChipType == _chips[index2.x, index2.y].ChipType &&
                            _chips[index2.x, index2.y].ChipType == _chips[index3.x, index3.y].ChipType;
            }

            return doesMatch;
        }

        private void SetHighlightChips(Vector2Int chipIndex, Vector2Int chipIndex1, Vector2Int chipIndex2)
        {
            _highlightedChips[0] = _chips[chipIndex.x, chipIndex.y];
            _highlightedChips[1] = _chips[chipIndex1.x, chipIndex1.y];
            _highlightedChips[2] = _chips[chipIndex2.x, chipIndex2.y];
        }

        private void StartAnimation()
        {
            if (_hasHighlight)
            {
                _hintTweenAnimation.StartAnimation(_highlightedChips);
            }
        }

        private void OnClick()
        {
            if (_hasHighlight)
            {
                _hintTweenAnimation.StopAnimation(_highlightedChips);
            }
        }
    }
}