namespace Manager
{
    public class Timer
    {
        private float _timerMaxValue;
        private float _timerMinValue;
        private float _currentTime;

        public float TimerMinValue => _timerMinValue;

        public float CurrentTime => _currentTime;

        public bool IsTimerStopped => !_isTimeStarted;

        private bool _isTimeStarted = false;

        public Timer(float timerMinValue = 0, float timerMaxValue = 1)
        {
            _timerMaxValue = timerMaxValue;
            _timerMinValue = timerMinValue;

            _currentTime = _timerMaxValue;
        }

        public void TimerTick(float tick)
        {
            _currentTime -= tick;
            TickAction();
        }
        
        public void StartTimeCount()
        {
            _currentTime = _timerMaxValue;
            _isTimeStarted = true;
        }

        public void StopTimeCount()
        {
            _isTimeStarted = false;
        }

        public void ResetTimeCount()
        {
            _currentTime = _timerMaxValue;
        }

        private void TickAction()
        {
            if (_isTimeStarted)
            {
                if (_currentTime <= _timerMinValue)
                {
                    _currentTime = _timerMinValue;
                    _isTimeStarted = false;
                }
            }
        }
    }
}