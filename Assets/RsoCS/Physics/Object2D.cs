using System;

namespace rso.physics
{
    // Collider 와 Transform 관계에서는 Collider의 Offset, Size 를 먼저 계산하고 이후 Transform 을 계산할것.
    // Transform 에서는 Rotation, Scale을 먼저 계산하고 이후 Position 을 계산할것.
    // Rotation 에서는 Z, X, Y 로 순서로 하되 축은 World 축 으로 고정이라고 가정하고 모델만 회전하는 방식으로 처리할것.
    // 객체간 상속관계에서는 자식에서 부모쪽으로 순서대로 처리할것.
    public class CObject2D : STransform
    {
        CObject2D _Parent = null;
        bool _Enabled = true;
        public bool Enabled
        {
            get
            {
                return _Enabled && (_Parent == null || _Parent.Enabled);
            }
            set
            {
                _Enabled = value;
            }
        }
        public SPoint Position
        {
            get
            {
                if (_Parent != null)
                    return _Parent.Position.GetAdd(LocalPosition);
                else
                    return LocalPosition;
            }
        }
        public CObject2D(STransform Transform_) :
            base(Transform_)
        {
        }
        public CObject2D(STransform Transform_, CObject2D Parent_) :
            base(Transform_)
        {
            _Parent = Parent_;
        }
        public void SetParent(CObject2D Parent_)
        {
            _Parent = Parent_;
        }
    }
}
