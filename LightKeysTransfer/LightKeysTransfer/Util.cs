using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TextCopy;

namespace LightKeysTransfer
{
    public static class Util
    {
        private static RSA rsa;


        public static string GetSensitiveText()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                password.Append(keyInfo.KeyChar);

                keyInfo = Console.ReadKey(true);
            }

            return password.ToString();
        }
        public static void GenerateRSAKeyPair()
        {
            rsa = RSACryptoServiceProvider.Create(2048);
        }
        public static void CopyPublicKey()
        {
            ClipboardService.SetText(rsa.ToXmlString(false));
        }
        public static void ClearClipBoard()
        {
            ClipboardService.SetText(" ");
        }

        public static string DecryptText(string text)
        {
            if (String.IsNullOrEmpty(text)) return text;

            try
            {
                var bytes = Convert.FromBase64String(text);
                var decryptedBytes = rsa.Decrypt(bytes, RSAEncryptionPadding.OaepSHA512);
                var plainText = UTF8Encoding.UTF8.GetString(decryptedBytes);
                int skipLength = plainText.Length / 3;
                return plainText.Substring(0, plainText.Length - skipLength);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        public static string EncryptText(string text)
        {
            if (String.IsNullOrEmpty(text)) return text;

            try
            {
                int saltLength = text.Length / 2;
                text = text + GetRandomSalt(saltLength);
                var bytes = UTF8Encoding.UTF8.GetBytes(text);
                var encryptedByte = rsa.Encrypt(bytes, RSAEncryptionPadding.OaepSHA512);
                var encryptedText = Convert.ToBase64String(encryptedByte);
                return encryptedText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        private static string GetRandomSalt(int length)
        {
            if (length == 0) return String.Empty;
            var b = RandomNumberGenerator.GetBytes(length);

            var str = Convert.ToBase64String(b);
            return str.Substring(0, length);
        }

        public static bool InitializeRSA(string publicKey)
        {
            try
            {
                if (rsa == null)
                {
                    GenerateRSAKeyPair();
                }

                rsa.FromXmlString(publicKey);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}
