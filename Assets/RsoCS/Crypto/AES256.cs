using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace rso
{
    namespace crypto
    {
        public class AES256
        {
            string _Key;
            public AES256(string Key_)
            {
                _Key = Key_;
            }
            public string Encrypt(string Data_)
            {
                using (var ms = new MemoryStream())
                {
                    var aes = new RijndaelManaged
                    {
                        KeySize = 256,
                        BlockSize = 128,
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7,
                        Key = Encoding.UTF8.GetBytes(_Key),
                        IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                    };

                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                    {
                        var Bytes = Encoding.UTF8.GetBytes(Data_);
                        cs.Write(Bytes, 0, Bytes.Length);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            public string Decrypt(string Data_)
            {
                using (var ms = new MemoryStream())
                {
                    var aes = new RijndaelManaged
                    {
                        KeySize = 256,
                        BlockSize = 128,
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7,
                        Key = Encoding.UTF8.GetBytes(_Key),
                        IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                    };

                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] xXml = Convert.FromBase64String(Data_);
                        cs.Write(xXml, 0, xXml.Length);
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}