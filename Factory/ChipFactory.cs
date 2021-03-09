using System;
using Enum;
using Model;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Factory
{
    public sealed class ChipFactory : IFactory<ChipModel>
    {
        public ChipModel Create()
        {
            var go = GameObject.Instantiate(Resources.Load<Transform>("TestSprite"));
            var randChipType = (ChipType) Random.Range(1, (int) ChipType.COUNT);

            var chipModel = new ChipModel(randChipType, go);

            var spriteRenderer = chipModel.SpriteRenderer;

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
            
            return chipModel;
        }
    }
}