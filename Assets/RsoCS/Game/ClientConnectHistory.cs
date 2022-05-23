using System.Collections.Generic;
using rso.net;

namespace rso
{
    namespace game
    {
        class CClientConnectHistory
        {
            HashSet<CNamePort> _Datas = new HashSet<CNamePort>();

            public bool Connect(CNamePort NamePort_)
            {
                try
                {
                    return _Datas.Add(NamePort_);
                }
                catch
                {
                    return false;
                }
            }
            public void Clear()
            {
                _Datas.Clear();
            }
        }
    }
}
