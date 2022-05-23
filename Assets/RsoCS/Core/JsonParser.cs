using System;

namespace rso
{
    namespace core
    {
        public static class JsonParser
        {
            public static string PopString(ref string Text_, ref Int32 Index_)
            {
                for (var StartIndex = ++Index_; Index_ < Text_.Length; ++Index_) // 외부에서 첫 문자 체크 한것으로 간주
                {
                    switch (Text_[Index_])
                    {
                        case '\n':
                        case '\r':
                        case '\t':
                        case '\v':
                            throw new Exception("Invalid Json String");

                        case '"':
                            return Text_.Substring(StartIndex, Index_++ - StartIndex);
                    }
                }

                throw new Exception("Invalid Json String (Expected '\"')");
            }
            public static void PopBlank(ref string Text_, ref Int32 Index_)
            {
                for (; Index_ < Text_.Length; ++Index_)
                {
                    switch (Text_[Index_])
                    {
                        case ' ':
                        case '\n':
                        case '\r':
                        case '\t':
                        case '\v':
                            break;

                        default:
                            return;
                    }
                }
            }
            public static JsonData Parse(ref string Text_, ref Int32 Index_)
            {
                PopBlank(ref Text_, ref Index_);

                switch (Text_[Index_])
                {
                    case 't':
                    case 'T':
                    case 'f':
                    case 'F':
                        return new JsonDataBool(Text_, ref Index_);

                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        return new JsonDataNumber(Text_, ref Index_);

                    case '"':
                        return new JsonDataString(Text_, ref Index_);

                    case '[':
                        return new JsonDataArray(Text_, ref Index_);

                    case '{':
                        return new JsonDataObject(Text_, ref Index_);

                    default:
                        throw new Exception("Invalid Json Value");
                }
            }
            public static JsonData Parse(string Text_)
            {
                Int32 Index = 0;
                return Parse(ref Text_, ref Index);
            }
        }
    }
}
