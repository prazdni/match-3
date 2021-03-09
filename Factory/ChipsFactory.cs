using ConfigScripts;
using Model;
using UnityEngine;

namespace Factory
{
    public sealed class ChipsFactory : IFactory<ChipModel[,]>
    {
        private readonly IFactory<ChipModel> _chipFactory;
        private readonly ChipDimensionLength _chipsConfig;
        private ChipModel[,] _chips;
        
        public ChipsFactory()
        {
            _chipsConfig = Resources.Load<ChipDimensionLength>("ChipDimensionLength");
            _chipFactory = new ChipFactory();
        }
        
        public ChipModel[,] Create()
        {
            return CreateTwoDimChips(_chipsConfig.width, _chipsConfig.height);
        }

        private ChipModel[,] CreateTwoDimChips(int width, int height)
        {
            _chips = new ChipModel[height, width];
            
            var parent = new GameObject("Chips");
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var chip = _chipFactory.Create();
                    var chipTransform = chip.Transform;
        
                    SetPosition(j - width / 2 + 0.5f, i - height/2 + 0.5f, chipTransform, parent.transform);
                    _chips[i, j] = chip;
                }
            }
            
            return _chips;
        }

        private void SetPosition(float x, float y, Transform chipTransform, Transform parentTransform)
        {
            var pos = new Vector2(x, y);
            chipTransform.position = pos;
            
            chipTransform.name = pos.ToString();
                    
            chipTransform.SetParent(parentTransform);
        }
    }
}