using Interface;
using TMPro;
using UnityEngine;

namespace View
{
    public sealed class TimerView : MonoBehaviour, IInitialize<IViewModel<float>>
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Initialize(IViewModel<float> obj)
        {
            obj.OnAction += ShowTime;
        }

        private void ShowTime(float time)
        {
            _text.text = Mathf.Ceil(time).ToString();
        }
    }
}