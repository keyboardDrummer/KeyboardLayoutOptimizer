using System;
using System.Collections.Concurrent;
using System.Threading;

namespace KeyboardEPCS.UI
{
    public class QueueWorker
    {
        readonly EventWaitHandle waitHandle = new EventWaitHandle(false,EventResetMode.AutoReset);
        readonly ConcurrentQueue<Action> workQueue;
        bool working = true;
 
        public QueueWorker()
        {
            workQueue = new ConcurrentQueue<Action>();
            new Thread(() =>
            {
                while (working)
                {
                    waitHandle.WaitOne();
                    Action action;
                    while (working && workQueue.TryDequeue(out action))
                        action();
                }
            }).Start();
        }

        public void AddWorkItem(Action action)
        {
            workQueue.Enqueue(action);
            waitHandle.Set();
        }

        public void StopWorking()
        {
            working = false;
        }
    }
}