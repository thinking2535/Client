using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rso
{
    namespace Base
    {
        public class CMemory<_T>
        {
            Dictionary<Int32, _T> _Filled = new Dictionary<Int32, _T>();
            HashSet<Int32> _Empty = new HashSet<Int32>();

            public CMemory()
            {
            }

            public Int32 New(_T Data_)
            {
                try
                {
                    var NewIndex = _Empty.First();
                    try
                    {
                        _Filled.Add(NewIndex, Data_);
                        _Empty.Remove(NewIndex);
                        return NewIndex;
                    }
                    catch
                    {
                        return -1;
                    }
                }
                catch
                {
                    try
                    {
                        _Filled.Add(_Filled.Count(), Data_);
                        return _Filled.Count() - 1;
                    }
                    catch
                    {
                        return -1;
                    }
                }
            }

            public void Del(Int32 Index_)
            {
                if (_Filled.ContainsKey(Index_))
                {
                    try
                    {
                        _Empty.Add(Index_);
                        _Filled.Remove(Index_);
                    }
                    catch
                    {
                    }
                }
            }

            public bool Contains(Int32 Index_)
            {
                return _Filled.ContainsKey(Index_);
            }
            public _T Get(Int32 Index_)
            {
                return _Filled[Index_];
            }
        }
    }
}