using System;
using System.Collections.Generic;

namespace rso
{
    namespace net
    {
        public class BinderSend<_TData>
        {
            Dictionary<Type, _TData> _Protos = new Dictionary<Type, _TData>();

            public void Bind<_TProtoType>(_TData Data_)
            {
                _Protos.Add(typeof(_TProtoType), Data_);
            }

            public _TData Get<_TProtoType>()
            {
                if (!_Protos.ContainsKey(typeof(_TProtoType)))
                    throw new Exception("Invalid Protocol");

                return _Protos[typeof(_TProtoType)];
            }
            public Int32 Count()
            {
                return _Protos.Count;
            }
        };
    }
}
