using System;

namespace KeyboardEPCS.UI
{
    public class Safe
    {
        int locks;
        public event Action SafeOpened;

        public void AddLock()
        {
            lock(this)
                locks++;
        }

        public void RemoveLock()
        {
            lock(this)
            {
                locks--;
                if (locks == 0)
                    SafeOpened();
            }
        }
    }
}