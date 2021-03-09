using DG.Tweening;
using Enum;

namespace Animation
{
    public interface IAnimationTweeners<T>
    {
        Tweener Get(AnimState state, T obj);
    }
}