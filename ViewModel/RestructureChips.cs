using Model;
using UnityEngine;

namespace ViewModel
{
    public sealed class RestructureChips
    {
        private int[] _moveDownCount;

        public void Restructure(ChipModel[,] chips)
        {
            for (int x = chips.GetLength(0) - 1; x >= 0; x--)
            {
                for (int y = 0; y < chips.GetLength(1); y++)
                {
                    if (chips[x, y].Marker)
                    {
                        ReplaceChip(x, y, chips);
                    }
                }
            }
        }

        private void ReplaceChip(int i, int j, ChipModel[,] chips)
        {
            var y = j;

            for (int x = i; x < chips.GetLength(0); x++)
            {
                if (!chips[x, y].Marker)
                {
                    ChangePlaces(chips, new Vector2Int(i, j), new Vector2Int(x, y));
                    i = x;
                }
            }
        }

        public void ChangePlaces(ChipModel[,] chips, Vector2Int pos1, Vector2Int pos2)
        {
            var pos = chips[pos1.x, pos1.y].Transform.position;
            chips[pos1.x, pos1.y].Transform.position = chips[pos2.x, pos2.y].Transform.position;
            chips[pos2.x, pos2.y].Transform.position = pos;

            var chip = chips[pos1.x, pos1.y];
            chips[pos1.x, pos1.y] = chips[pos2.x, pos2.y];
            chips[pos2.x, pos2.y] = chip;
        }
    }
}