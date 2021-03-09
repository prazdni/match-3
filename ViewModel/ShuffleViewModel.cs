using System;
using Interface;
using Model;
using Pull;
using UnityEngine;

namespace ViewModel
{
    public class ShuffleViewModel : IViewModel
    {
        public event Action OnAction = () => { };
        
        private readonly IViewModel _searchViewModel;
        private readonly IPull<Transform, ChipModel> _chipPull;
        private readonly ChipModel[,] _chips;

        public ShuffleViewModel(ChipPull chipPull, IViewModel searchViewModel)
        {
            _searchViewModel = searchViewModel;
            _chipPull = chipPull;
            _chips = chipPull.Chips;
        }
        
        public void Action()
        {
            for (int x = 0; x < _chips.GetLength(0); x++)
            {
                for (int y = 0; y < _chips.GetLength(1); y++)
                {
                    _chipPull.Return(_chips[x, y]);
                }
            }
            
            OnAction.Invoke();
            
            _searchViewModel.Action();
        }
    }
}