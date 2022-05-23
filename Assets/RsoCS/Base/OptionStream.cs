using rso.core;
using System;

namespace rso
{
    namespace Base
    {
        public class COptionStream<TData> : COption<TData> where TData : new()
        {
            public COptionStream(String FileName_, bool NoException_) :
                base(FileName_)
            {
                var Stream = new CStream();

                try
                {
                    Stream.LoadFile(FileName_);
                }
                catch
				{
                    if (!NoException_)
                        throw;

                    Save();
                    return;
                }

                try
                {
                    Stream.Pop(ref _Data);
                }
                catch
				{
                    if (!NoException_)
                        throw;

                    Reset();
                    Save();
                }
            }
            public COptionStream(string FileName_, TData Data_) :
                base(FileName_, Data_)
            {
                Save();
            }
            public override void Save()
            {
                new CStream().Push(_Data).SaveFile(_FileName);
            }
        }
    }
}