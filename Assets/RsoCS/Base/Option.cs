using System;
using System.IO;

namespace rso
{
    namespace Base
    {
        public abstract class COption<TData> where TData : new()
        {
            protected String _FileName = "";
            protected TData _Data = new TData();
            public COption(string FileName_)
            {
                _FileName = FileName_;
            }
            public COption(string FileName_, TData Data_)
            {
                _FileName = FileName_;
                _Data = Data_;
            }
            public TData Data
            {
                get
                {
                    return _Data;
                }
                set
                {
                    _Data = value;
                    Save();
                }
            }
            public void Reset()
            {
                _Data = new TData();
            }
            public void Clear()
            {
                Reset();
                Save();
            }
            public abstract void Save();
        }
    }
}