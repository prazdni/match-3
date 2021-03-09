using Interface;
using TMPro;
using UnityEngine;

namespace View
{
    public sealed class EndGameView : MonoBehaviour, IInitialize<IViewModel<bool>>
    {
        [SerializeField] private Transform _image;
        [SerializeField] private Transform _imageWin;
        [SerializeField] private Transform _imageLose;
        [SerializeField] private TMP_Text _text;

        public void Initialize(IViewModel<bool> obj)
        {
            obj.OnAction += OnGameEnd;
        }

        private void OnGameEnd(bool win)
        {
            Time.timeScale = 0;
            
            Show(win);
        }

        private void Show(bool win)
        {
            _image.gameObject.SetActive(true);
            
            _text.text = win ? "Congratulations!" : "You almost did it!";
            
            if (win)
            {
                _imageWin.gameObject.SetActive(true);
            }
            else
            {
                _imageLose.gameObject.SetActive(true);
            }
        }
    }
}