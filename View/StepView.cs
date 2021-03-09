using Interface;
using TMPro;
using UnityEngine;

namespace View
{
    public sealed class StepView : MonoBehaviour, IInitialize<IViewModel<int>>
    {
        private TMP_Text _text;
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        
        public void Initialize(IViewModel<int> obj)
        {
            obj.OnAction += WriteText;
            obj.Action(0);
        }

        private void WriteText(int step)
        {
            _text.text = step.ToString();
        }
    }
}