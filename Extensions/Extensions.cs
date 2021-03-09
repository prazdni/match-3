using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static T GetOrAddComponent<T>(this Transform transform) where T : Component
        {
            T component;
            
            if (transform.gameObject.TryGetComponent(out component))
            {
                return component;
            }

            return transform.gameObject.AddComponent<T>();
        }
    }
}