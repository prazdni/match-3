using UnityEngine;

namespace Animation
{
    public interface ITweenAnimation<T>
    {
        void StartAnimation(T obj);
        void StopAnimation(T obj);
    }
}