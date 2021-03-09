using Interface;
using UnityEngine;
using ViewModel;

namespace Manager
{
    public sealed class InputManager : IExecute
    {
        private readonly IViewModel<Transform> _replaceChipViewModel;
        private readonly SearcherViewModel _searcherViewModel;
        private readonly Camera _camera;
        private readonly IViewModel<KeyCode> _viewModel;

        public InputManager(Camera camera, IViewModel<Transform> replaceChipViewModel, SearcherViewModel searcherViewModel,
            IViewModel<KeyCode> buttonViewModel)
        {
            _camera = camera;
            _replaceChipViewModel = replaceChipViewModel;
            _searcherViewModel = searcherViewModel;
            _viewModel = buttonViewModel;
        }
        
        public void Execute(float deltaTime)
        {
            if (Time.timeScale != 0.0f && !_searcherViewModel.IsAnimating)
            {
                var mousePos = Input.mousePosition;
                if (Input.GetMouseButtonUp(0))
                {
                    var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(mousePos), Vector2.zero);
                
                    if (hit && hit.transform.CompareTag("Chip"))
                    {
                        _replaceChipViewModel.Action(hit.transform);
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))           
            {
                _viewModel.Action(KeyCode.Escape);
            }
        }
    }
}