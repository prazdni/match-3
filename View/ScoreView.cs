using Interface;
using TMPro;
using UnityEngine;

namespace View
{
    public sealed class ScoreView : MonoBehaviour, IInitialize<IViewModel<int, int>>
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Initialize(IViewModel<int, int> obj)
        {
            obj.OnAction += ViewScore;
            obj.Action(0, 0);
        }

        private void ViewScore(int score, int totalScore)
        {
            _text.text = $"{score}/{totalScore}";
        }
    }
}