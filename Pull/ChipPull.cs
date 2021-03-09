using System;
using Enum;
using Factory;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pull
{
    public sealed class ChipPull : IPull<Transform, ChipModel>, IPull<Transform, Vector2Int>
    {
        public ChipModel[,] Chips => _chips;
        private ChipModel[,] _chips;

        public ChipPull()
        {
            IFactory<ChipModel[,]> chipsFactory = new ChipsFactory();
            
            _chips = chipsFactory.Create();
        }

        public ChipModel Get(Transform obj)
        {
            ChipModel chip = null;
            
            for (int x = 0; x < _chips.GetLength(0); x++)
            {
                for (int y = 0; y < _chips.GetLength(1); y++)
                {
                    if (_chips[x,y].Transform == obj.transform)
                    {
                        chip = _chips[x, y];
                    }
                }
            }

            return chip;
        }

        public void Return(ChipModel obj)
        {
            ChangeType(obj);
        }

        Vector2Int IPull<Transform, Vector2Int>.Get(Transform obj)
        {
            Vector2Int chipIndex = Vector2Int.zero;
            
            for (int x = 0; x < _chips.GetLength(0); x++)
            {
                for (int y = 0; y < _chips.GetLength(1); y++)
                {
                    if (_chips[x,y].Transform == obj.transform)
                    {
                        chipIndex = new Vector2Int(x, y);
                    }
                }
            }

            return chipIndex;
        }
        
        public void Return(Vector2Int obj)
        {
            _chips[obj.x, obj.y].Transform.gameObject.SetActive(false);
        }
        
        private void ChangeType(ChipModel obj)
        {
            var randChipType = (ChipType) Random.Range(1, (int) ChipType.COUNT);
            
            obj.ChipType = randChipType;
            
            var spriteRenderer = obj.SpriteRenderer;

            switch (randChipType)
            {
                case ChipType.Red:
                    spriteRenderer.color = Color.red;
                    break;
                case ChipType.Green:
                    spriteRenderer.color = Color.green;
                    break;
                case ChipType.Blue:
                    spriteRenderer.color = Color.blue;
                    break;
                case ChipType.Yellow:
                    spriteRenderer.color = Color.yellow;
                    break;
                case ChipType.Black:
                    spriteRenderer.color = Color.black;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}