namespace rso.http
{
    public enum EHttpRet
    {
        Ok,
        ObjectNotFound,
        NotEnoughMemory,
        Max,
        Null
    }

    public class SInObj
    {
        public string ServerName;
        public string ObjectName;
        public SInObj(string ServerName_, string ObjectName_)
        {
            ServerName = ServerName_;
            ObjectName = ObjectName_;
        }
    }
}