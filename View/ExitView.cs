using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class ExitView : MonoBehaviour
    {
        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            Application.Quit();
            
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}