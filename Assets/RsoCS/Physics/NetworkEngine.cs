
using System;

namespace rso.physics
{
    public abstract class CNetworkEngine : CEngine
    {
        protected Int64 _NetworkTickSync;

        public CNetworkEngine(Int64 CurTick_, Single ContactOffset_, Int32 FPS_, Int64 NetworkTickSync_) :
            base(CurTick_, ContactOffset_, FPS_)
        {
            _NetworkTickSync = NetworkTickSync_;
        }
    }
}
