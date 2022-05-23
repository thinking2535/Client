using System.Threading;

namespace rso
{
    namespace Base
    {
        public class CThread
        {
            public delegate void TThreadFunc();
            public volatile bool Exit = false;
            Thread _Thread;

            public CThread(TThreadFunc ThreadFunc_)
            {
                _Thread = new Thread(() => ThreadFunc_());
                _Thread.Start();
            }
            ~CThread()
            {
                Dispose();
            }
            public void Dispose()
            {
                Exit = true;
                _Thread.Join();
            }
        }
    }
}
