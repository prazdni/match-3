using DG.Tweening;
using Enum;
using Model;
using UnityEngine;

namespace Animation
{
    public sealed class SlideAnimation : ITweenAnimation<ChipModel[,]>
    {
        private readonly IAnimationSequences<ChipModel> _animationSequences;
        public bool IsAnimating => _isPlaying;
        private Sequence _sequence;
        private bool _isPlaying;

        public SlideAnimation(IAnimationSequences<ChipModel> animationSequences)
        {
            _animationSequences = animationSequences;
        }

        public void StartAnimation(ChipModel[,] obj)
        {
            _sequence = DOTween.Sequence();
            
            _sequence.OnComplete(OnComplete);
            
            _isPlaying = true;
            
            for (int x = 0; x < obj.GetLength(0); x++)
            {
                for (int y = 0; y < obj.GetLength(1); y++)
                {
                    if (obj[x, y].Marker)
                    {
                        obj[x, y].Transform.localScale = Vector3.zero;
                        Sequence seq = _animationSequences.Get(AnimState.Slide, obj[x, y]);

                        _sequence.Join(seq);
                    }
                }
            }

            _sequence.Play();
        }

        private void OnComplete()
        {
            _isPlaying = false;
            _sequence.Kill();
        }

        public void StopAnimation(ChipModel[,] obj)
        {
            _sequence.Pause();
            _isPlaying = false;
            _sequence.Kill();
        }
    }
}