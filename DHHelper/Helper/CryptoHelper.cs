using System.Security.Cryptography;
using System.Text;

namespace DHHelper.Helper
{
    public static class CryptoHelper
    {

        /// <summary>
        /// Parse Base64Encode
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Encode(string data)
        {

            try
            {

                byte[] encData_byte = new byte[data.Length];

                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);

                string encodedData = Convert.ToBase64String(encData_byte);

                return encodedData;

            }
            catch (Exception e)
            {
                throw new Exception("Error in Base64Encode: " + e.Message);
            }
        }

        /// <summary>
        /// Parse Base64 Decode
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);

                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

                char[] decoded_char = new char[charCount];

                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

                string result = new String(decoded_char);

                return result;

            }
            catch (Exception e)
            {
                throw new Exception("Error in Base64Decode: " + e.Message);
            }

        }

        /// <summary>
        /// Make HashString
        /// </summary>
        /// <param name="text"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string SHA512Hash(string text, string secretKey)
        {
            var hash = new StringBuilder();
            byte[] secretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);
            using (var hmac = new HMACSHA512(secretkeyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        /// <summary>
        /// Encrypt AES256
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="key">32자리 글자로 구성</param>
        /// <returns></returns>
        public static string AESEncrypt256(string Input, string key)
        {
            Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[]? xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            string Output = Convert.ToBase64String(xBuff);
            return Output;

        }


        /// <summary>
        /// Decrypt aes 256
        /// </summary>
        /// <param name="Input"></param>
        /// <param name="key">32자리 글자로 구성</param>
        /// <returns></returns>
        public static string AESDecrypt256(string Input, string key)
        {
            Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decrypt = aes.CreateDecryptor();

            byte[]? xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            string Output = Encoding.UTF8.GetString(xBuff);
            return Output;
        }

    }
}