using System;
using System.Threading;

namespace Generic.Uncommon
{
    [Serializable()]
    public class BackgroundThread
    {
        public BackgroundThread(Action start)
        {
            threadStart = new ThreadStart(start);
            thread = new Thread(threadStart);
        }

        public interface IHandler{}
        private readonly Thread thread;
        private readonly ThreadStart threadStart;
        private StateType state;
        public StateType State
        {
            get
            {
                return state;
            }
            set
            {
                var oldState = state;
                state = value;
                if (oldState != state)
                    StateChanged(oldState);
            }
        }

        public enum StateType { Pausing, Paused, Working, Stopping, Stopped };
        static EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);

        public void Start()
        {
            State = StateType.Working;
            thread.Start();
        }

        protected BackgroundThread()
        {
            State = StateType.Working;
        }

        public void Stop()
        {
            if (State != StateType.Stopped)
                State = StateType.Stopping;
        }

        public void Pause()
        {
            State = StateType.Pausing;
        }

        public void Resume()
        {
            State = StateType.Working;
            ewh.Reset();
        }

        public Boolean Working { get { return State == StateType.Working; } }
        public Boolean Stopped { get { return State == StateType.Stopped; } }
        public delegate void StateChangedHandler(StateType oldState);
        public event StateChangedHandler StateChanged = delegate { };

        public Boolean CheckForBreak()
        {
            switch (State)
            {
                case StateType.Pausing: State = StateType.Paused; ewh.Set(); 
                    while (State == StateType.Paused) { Thread.Sleep(100); } 
                    return true;
                case StateType.Working: return true;
                case StateType.Stopping: State = StateType.Stopped; return false;
                default: return true;
            }
        }
    }
}
