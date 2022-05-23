using System;

namespace rso
{
    namespace proto
    {
        public class Parser
        {
            string _Str;
            Int32 _Index = 0;

            public Parser(string Str_)
            {
                _Str = Str_;
            }
            public string Get()
            {
                if (_Index >= _Str.Length)
                    return "";

                Int32 OldIndex = 0;
                for (var Pos = _Index; Pos < _Str.Length; ++Pos)
                {
                    if (_Str[Pos] == '{')
                    {
                        if (Pos == _Index)
                        {
                            ++_Index;
                            return "{";
                        }

                        OldIndex = _Index;
                        _Index = Pos;
                        return _Str.Substring(OldIndex, Pos - OldIndex);
                    }
                    else if (_Str[Pos] == '}')
                    {
                        if (Pos == _Index)
                        {
                            ++_Index;
                            return "}";
                        }

                        OldIndex = _Index;
                        _Index = Pos;
                        return _Str.Substring(OldIndex, Pos - OldIndex);
                    }
                    else if (_Str[Pos] == ',')
                    {
                        if (Pos == _Index)
                        {
                            ++_Index;
                            continue;
                        }

                        OldIndex = _Index;
                        _Index = Pos;
                        return _Str.Substring(OldIndex, Pos - OldIndex);
                    }
                }

                OldIndex = _Index;
                _Index = _Str.Length;
                return _Str.Substring(OldIndex);
            }
        };
    }
}