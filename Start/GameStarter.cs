using Animation;
using Manager;
using Pull;
using UnityEngine;
using View;
using ViewModel;


namespace Start
{
    public sealed class GameStarter : MonoBehaviour
    {
        [SerializeField] private TimerView _timerView;
        [SerializeField] private StepView _stepView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private EndGameView _endGameView;
        [SerializeField] private ShowSettingsView _showSettingsView;
        
        private InputManager _inputManager;
        private TableManager _tableManager;

        private void Awake()
        {
            Time.timeScale = 1.0f;
        }

        private void Start()
        {
            var chipPull = new ChipPull();
            var animationTweeners = new AnimationSequences();
            
            var buttonViewModel = new ButtonPressedViewModel();
            var clickViewModel = new ClickViewModel();
            var scoreViewModel = new ScoreViewModel(50, clickViewModel);
            var stepViewModel = new StepViewModel(10);
            var searcher = new SearcherViewModel(chipPull, scoreViewModel, clickViewModel, animationTweeners);
            var shuffleViewModel = new ShuffleViewModel(chipPull, searcher);
            var highlight = new HighlightChipViewModel(chipPull, clickViewModel, new HintTweenAnimation(animationTweeners));
            var replace = new ReplaceChipViewModel(chipPull, searcher, clickViewModel, 
                stepViewModel, new ChooseTweenAnimation(animationTweeners));
            var timerViewModel = new TimerViewModel(30.0f, 2.0f, highlight, clickViewModel, 
                searcher);
            var endGameViewModel = new EndGameViewModel(stepViewModel, scoreViewModel, timerViewModel);
            
            _inputManager = new InputManager(Camera.main, replace, searcher, buttonViewModel);
            _tableManager = new TableManager(searcher, highlight, timerViewModel, endGameViewModel, shuffleViewModel);
            
            _stepView.Initialize(stepViewModel);
            _scoreView.Initialize(scoreViewModel);
            _timerView.Initialize(timerViewModel);
            _endGameView.Initialize(endGameViewModel);
            _showSettingsView.Initialize(buttonViewModel);
        }

        private void Update()
        {
            _inputManager.Execute(Time.deltaTime);
            _tableManager.Execute(Time.deltaTime);
        }
    }
}