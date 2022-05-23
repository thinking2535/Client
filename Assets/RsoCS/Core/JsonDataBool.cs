using System;

namespace rso
{
    namespace core
    {
        public class JsonDataBool : JsonData
        {
            bool _Data;
            public JsonDataBool(bool Value_)
            {
                _Data = Value_;
            }
            public JsonDataBool(string Text_, ref Int32 Index_)
            {
                if (Text_.Length - Index_ < 4)
                    throw new Exception("Invalid Json Bool");

                if (Text_.Substring(Index_, 4).ToLower() == "true")
                {
                    _Data = true;
                    Index_ += 4;
                    return;
                }

                if (Text_.Length - Index_ < 5)
                    throw new Exception("Invalid Json Bool");

                if (Text_.Substring(Index_, 5).ToLower() == "false")
                {
                    _Data = false;
                    Index_ += 5;
                    return;
                }
            }
            public override string ToString(string Name_, string Indentation_)
            {
                return Indentation_ + JsonGlobal.GetNameString(Name_) + _Data.ToString();
            }
            public override bool GetBool() { return _Data; }
            public void SetData(bool Data_)
            {
                _Data = Data_;
            }
        }
    }
}