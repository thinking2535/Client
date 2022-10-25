using System;

namespace rso.physics
{
    public class CLocalEngine : CEngine
    {
        public CLocalEngine(Int64 CurTick_, Single ContactOffset_, Int32 FPS_) :
            base(CurTick_, ContactOffset_, FPS_)
        {
        }
        public override void Update()
        {
            _Update(_CurTick.Get());
        }
    }
}