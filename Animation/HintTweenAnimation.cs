using DG.Tweening;
using Enum;
using Model;

namespace Animation
{
    public sealed class HintTweenAnimation : ITweenAnimation<ChipModel[]>
    {
        private readonly IAnimationSequences<ChipModel> _animationTweeners;
        private Sequence _sequence;

        public HintTweenAnimation(IAnimationSequences<ChipModel> animationTweeners)
        {
            _animationTweeners = animationTweeners;
        }

        public void StartAnimation(ChipModel[] obj)
        {
            _sequence = DOTween.Sequence();
                
            for (int i = 0; i < obj.Length; i++)
            {
                _sequence.Join(_animationTweeners.Get(AnimState.Hint, obj[i])).SetLoops(-1);
            }   
        }

        public void StopAnimation(ChipModel[] obj)
        {
            _sequence.Kill();
            
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].Transform.localScale = obj[i].DefaultScale;
            }
        }
    }
}