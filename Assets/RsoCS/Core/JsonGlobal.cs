namespace rso
{
    namespace core
    {
        public static class JsonGlobal
        {
            const string _Tab = "   ";
            public static string GetNameString(string Name_)
            {
                return Name_ == null ? "" : ("\"" + Name_ + "\" : ");
            }
            public static string PushIndentation(this string Str_)
            {
                return Str_ += _Tab;
            }
            public static string PopIndentation(this string Str_)
            {
                return Str_.Remove(Str_.Length - _Tab.Length, _Tab.Length);
            }
        }
    }
}