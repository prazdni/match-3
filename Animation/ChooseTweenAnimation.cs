using DG.Tweening;
using Enum;
using Model;
using UnityEngine;

namespace Animation
{
    public sealed class ChooseTweenAnimation : ITweenAnimation<ChipModel[]>
    {
        private readonly IAnimationSequences<ChipModel> _animationTweeners;
        private Sequence _sequence;

        public ChooseTweenAnimation(IAnimationSequences<ChipModel> animationTweeners)
        {
            _animationTweeners = animationTweeners;
        }

        public void StartAnimation(ChipModel[] obj)
        {
            _sequence = DOTween.Sequence();
            _sequence.Join(_animationTweeners.Get(AnimState.Choose, obj[0]));
        }

        public void StopAnimation(ChipModel[] obj)
        {
            _sequence.Kill();
            
            for (int i = 0; i < 2; i++)
            {
                obj[i].Transform.localScale = obj[i].DefaultScale;
            }
        }
    }
}