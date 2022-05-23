using System;
using System.Security.Cryptography;
using System.Text;

namespace rso
{
    namespace crypto
    {
        public class Sha256
        {
            public static Byte[] ToStream(string String_)
            {
                return new SHA256CryptoServiceProvider().ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(String_));
            }
            public static string ToString(string String_)
            {
                string EncString = string.Empty;

                foreach (Byte x in ToStream(String_))
                    EncString += String.Format("{0:x2}", x);

                return EncString;
            }
        }
    }
}