using rso.gameutil;
using System;
using System.Collections.Generic;

namespace rso.physics
{
    public class CEngine
    {
        protected Int64 _NetworkTickSync;
        protected CTick _CurTick = null;
        public Int64 Tick { get; private set; } = 0; // 물리 처리가 끝난 시점의 Tick

        public static Single ContactOffset { get; private set; }
        public static Int32 FPS { get; private set; }
        public static Int64 UnitTick { get; private set; }
        public static Single DeltaTime { get; private set; }

        public CCollider2D Structure { get; private set; }
        public List<CMovingObject2D> MovingStructures { get; private set; }
        public List<CPlayerObject2D> Players { get; private set; }

        public CEngine(Int64 NetworkTickSync_, Int64 CurTick_, Single ContactOffset_, Int32 FPS_, CCollider2D Structure_, List<CMovingObject2D> MovingStructures_, List<CPlayerObject2D> Players_)
        {
            _NetworkTickSync = NetworkTickSync_;
            _CurTick = new CTick(CurTick_);
            _CurTick.Start();
            Tick = CurTick_;
            ContactOffset = ContactOffset_;
            FPS = FPS_;
            UnitTick = 10000000 / FPS;
            DeltaTime = 1.0f / FPS;
            Structure = Structure_;
            MovingStructures = MovingStructures_;
            Players = Players_;
        }
        protected void _Update(Int64 ToTick_)
        {
            for (; Tick < ToTick_; Tick += UnitTick)
            {
                OnFixedUpdate?.Invoke(Tick);

                foreach (var i in MovingStructures)
                    i.FixedUpdate(Tick);

                foreach (var p in Players)
                    p.FixedUpdate(Tick);

                for (Int32 pi = 0; pi < Players.Count - 1; ++pi)
                {
                    for (Int32 ti = pi + 1; ti < Players.Count; ++ti)
                        Players[pi].CollisionEnterCheck(Tick, Players[ti]);
                }

                foreach (var p in Players)
                {
                    foreach (var m in MovingStructures)
                        p.CollisionEnterCheck(Tick, m);

                    p.CollisionEnterCheck(Tick, Structure);
                    p.CollisionStayCheck(Tick);
                    p.CollisionExitCheck(Tick);
                }
            }
        }
        public Action<Int64> OnFixedUpdate;
    }
}