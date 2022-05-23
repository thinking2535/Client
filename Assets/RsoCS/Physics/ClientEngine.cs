using System;
using System.Collections.Generic;

namespace rso.physics
{
    public class CMessage
    {
        public Int64 Tick;
        public CMessage(Int64 Tick_)
        {
            Tick = Tick_;
        }
        public virtual void Proc()
        {
        }
    }
    class CClientEngine : CEngine
    {
        Int64 _NetworkTickBuffer;
        Queue<CMessage> _MessageQueue = new Queue<CMessage>();
        public CClientEngine(Int64 NetworkTickSync_, Int64 NetworkTickBuffer_, Int64 CurTick_, Single ContactOffset_, Int32 FPS_, CCollider2D Structure_, List<CMovingObject2D> MovingStructures_, List<CPlayerObject2D> Players_) :
            base(NetworkTickSync_, CurTick_, ContactOffset_, FPS_, Structure_, MovingStructures_, Players_)
        {
            _NetworkTickBuffer = NetworkTickBuffer_;
        }
        public void Sync(CMessage Message_)
        {
            _MessageQueue.Enqueue(Message_);

            if (_MessageQueue.Count == 1)
                _CurTick.Start();

            if (Message_.Tick > _CurTick.Get() + _NetworkTickBuffer)
            {
                _CurTick.Set(Message_.Tick - _NetworkTickBuffer);
                Update();
            }
        }
        public void Update()
        {
            var CurTick = _CurTick.Get();

            while (_MessageQueue.Count > 0)
            {
                if (CurTick < _MessageQueue.Peek().Tick)
                {
                    _Update(CurTick);
                    break;
                }

                var Message = _MessageQueue.Dequeue();
                if (_MessageQueue.Count == 0)
                    _CurTick.Stop();

                _Update(Message.Tick);
                Message.Proc();
            }
        }
    }
}