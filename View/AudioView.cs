using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class AudioView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;

        private Image _image;
        private Button _button;
        private float _defaultVolume;
        private bool _isPressed;

        private void Awake()
        {
            _defaultVolume = _audioSource.volume;
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _isPressed = !_isPressed;

            ChangeAudio();
        }

        private void ChangeAudio()
        {
            _audioSource.volume = _isPressed ? 0.0f : _defaultVolume;
            
            if (_isPressed)
            {
                _audioSource.Pause();
                _image.sprite = _soundOff;
            }
            else
            {
                _audioSource.Play();
                _image.sprite = _soundOn;
            }
        }
    }
}