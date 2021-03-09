using Enum;
using UnityEngine;

namespace Model
{
    public class ChipModel
    {
        public Transform Transform { get; }
        public SpriteRenderer SpriteRenderer { get; }
        public Vector3 DefaultScale { get; }
        
        public ChipType ChipType;
        public bool Marker;
        
        public ChipModel(ChipType chipType, Transform transform)
        {
            Transform = transform;
            SpriteRenderer = transform.GetComponent<SpriteRenderer>();
            DefaultScale = transform.localScale;
            ChipType = chipType;
        }
    }
}