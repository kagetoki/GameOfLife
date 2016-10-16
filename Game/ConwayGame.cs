using System;
using System.Threading;

namespace Game
{
    public class ConwayGame : IDisposable
    {
        public int Tact { get; private set; }//millisecs
        private Timer _timer;
        private GenerationLogic _generationLogic;
        private Field _prevField;
        private Field _field;
        public Field Field => _field;
        public bool IsStarted { get; private set; }
        private bool _isDisposed;
        public event EventHandler<NewGenerationEventArgs> GenerationChanged;

        public ConwayGame():this (50,50, 500)
        {

        }
        public ConwayGame(int width, int height, int tact)
        {
            Tact = tact;
            _timer = new Timer((s) => ChangeGeneration(), this, Timeout.Infinite, Tact);
            _field = new Field(width, height);
            _generationLogic = new GenerationLogic();
        }

        private void ChangeGeneration()
        {
            _prevField = _field;
            _field = _generationLogic.NewGeneration(_field);
            GenerationChanged?.Invoke(this, new NewGenerationEventArgs(_prevField, _field));
        }

        public void Start()
        {
            ThrowIfDisposed();
            _timer.Change(0, Tact);
            IsStarted = true;
        }

        public void Stop()
        {
            ThrowIfDisposed();
            _timer.Change(Timeout.Infinite, Tact);
            IsStarted = false;
        }

        public void Dispose()
        {
            if(_timer != null)
            {
                _timer.Dispose();
            }
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed) { throw new ObjectDisposedException(this.ToString()); }
        }

        ~ConwayGame()
        {
            Dispose();
        }
    }
}
