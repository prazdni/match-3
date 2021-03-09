using DG.Tweening;
using Enum;

namespace Animation
{
    public interface IAnimationSequences<T>
    {
        Sequence Get(AnimState state, T obj);
    }
}