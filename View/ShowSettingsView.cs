using Interface;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class ShowSettingsView : MonoBehaviour, IInitialize<IViewModel<KeyCode>>
    {
        [SerializeField] private Transform _transformSettings;
        private Button _button;
        private bool _isShown;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }
        
        public void Initialize(IViewModel<KeyCode> obj)
        {
            obj.OnAction += OnButtonPressed;
        }

        private void OnClick()
        {
            _isShown = !_isShown;
            ShowSettings(_isShown);
        }

        private void OnButtonPressed(KeyCode keyCode)
        {
            if (keyCode == KeyCode.Escape)
            {
                _isShown = !_isShown;
                ShowSettings(_isShown);
            }
        }
        
        private void ShowSettings(bool show)
        {
            _transformSettings.gameObject.SetActive(show);
            Time.timeScale = show ? 0.0f : 1.0f;
        }
    }
}