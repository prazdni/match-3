using Model;
using Pull;

namespace ViewModel
{
    public sealed class TableCleaner
    {
        private readonly ScoreViewModel _scoreViewModel;
        private readonly ChipPull _chipPull;
        
        public TableCleaner(ChipPull chipPull, ScoreViewModel scoreViewModel)
        {
            _chipPull = chipPull;
            _scoreViewModel = scoreViewModel;
        }

        public void CleanTable(ChipModel[,] chips)
        {
            var score = 0;
            
            for (int x = 0; x < chips.GetLength(0); x++)
            {
                for (int y = 0; y < chips.GetLength(1); y++)
                {
                    if (chips[x, y].Marker)
                    {
                        if (chips[x, y].Marker)
                        {
                            score++;
                        }
                        
                        chips[x, y].Marker = false;
                        _chipPull.Return(chips[x, y]);
                    }
                }
            }

            _scoreViewModel.Action(score, 0);
        }
    }
}