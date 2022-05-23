using rso.Base;
using rso.core;
using System;
using System.IO;
using System.Text;

namespace rso
{
    namespace gameutil
    {
        public class COptionJson<TData> : COption<TData> where TData : SProto, new()
        {
            public COptionJson(string FileName_, bool NoException_) :
                base(FileName_)
            {
                CStream Stream = new CStream();

                try
                {
                    Stream.LoadFile(_FileName);
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
                    Int32 Index = 0;
                    _Data.Push(new JsonDataObject(Stream.ToString(), ref Index));
                }
                catch
				{
                    if (!NoException_)
                        throw;

                    Reset();
                    Save();
                }
            }
            public COptionJson(string FileName_, TData Data_) :
				base(FileName_, Data_)
            {
                Save();
            }
            public override void Save()
            {
                var FullPath = Path.GetFullPath(_FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(FullPath));
                File.WriteAllText(FullPath, _Data.ToJsonString());
            }
        }
    }
}