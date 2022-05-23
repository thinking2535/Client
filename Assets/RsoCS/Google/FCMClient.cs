namespace rso
{
    namespace google
    {
        public class CFCMClient
        {
            string _Token = null;

            public void SetToken(string Token_)
            {
                _Token = Token_;
            }
            public string GetToken()
            {
                var Token = _Token;
                _Token = null;
                return Token;
            }
        }
    }
}
