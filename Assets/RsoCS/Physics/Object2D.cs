using System;

namespace rso.physics
{
    public class CObject2D
    {
		CObject2D _Parent = null;
		public SPoint LocalPosition;
		public CObject2D(SPoint LocalPosition_)
        {
            LocalPosition = LocalPosition_;
        }
        public void SetParent(CObject2D Parent_)
        {
            _Parent = Parent_;
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
	}
}
