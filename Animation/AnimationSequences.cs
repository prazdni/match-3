using DG.Tweening;
using Enum;
using Model;
using UnityEngine;

namespace Animation
{
    public sealed class AnimationSequences : IAnimationSequences<ChipModel>
    {
        private Sequence _sequence;

        public AnimationSequences()
        {
            _sequence = DOTween.Sequence();
        }

        Sequence IAnimationSequences<ChipModel>.Get(AnimState state, ChipModel obj)
        {
            _sequence = DOTween.Sequence();
            
            switch (state)
            {
                case AnimState.Choose:
                    return _sequence.Join(obj.Transform.DOScale(obj.DefaultScale * 1.1f,  1.0f));
                case AnimState.Hint:
                    return _sequence.Join(obj.Transform.DOShakeScale(2.0f, 0.5f, fadeOut: false));
                case AnimState.Slide:
                    var position = obj.Transform.position;
                    _sequence
                        .Join(obj.Transform.DOScale(obj.DefaultScale, 0.1f))
                        .Join(obj.Transform
                            .DOMove(position + Vector3.up * 3, 0.01f))
                        .Append(obj.Transform
                            .DOMove(position, 0.1f));
                    return _sequence;
                default:
                    return null;
            }
        }
    }
}