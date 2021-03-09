using UnityEngine;

namespace ConfigScripts
{
    [CreateAssetMenu(fileName = "ChipDimensionLength", menuName = "ChipDimensionLength", order = 0)]
    public class ChipDimensionLength : ScriptableObject
    {
        public int width;
        public int height;
    }
}