using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _4PD
{
    class Crypto
    {
        private RijndaelManaged AES;
        AesCryptoServiceProvider provider;

        public Crypto()
        {
            AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;
        }

        public void initialize(string keyString)
        {
            provider = new AesCryptoServiceProvider();
            keyString = keyString.Length <= 16 ? keyString : keyString.Substring(0, 16);
            if (keyString.Length < 16)
                while (keyString.Length < 16)
                    keyString += '0';

            provider.Mode = CipherMode.ECB;
            provider.BlockSize = 128;
            provider.KeySize = 128;
            provider.Key = Encoding.Default.GetBytes(keyString);
            provider.Padding = PaddingMode.PKCS7;
        }

        public void FileEncrypt(String filePath)
        {
            byte[] fileContent = File.ReadAllBytes(filePath);
            ICryptoTransform transform = provider.CreateEncryptor();
            byte[] encrypted = transform.TransformFinalBlock(fileContent, 0, fileContent.Length);
            File.WriteAllBytes(filePath, encrypted);
        }

        public void FileDecrypt(String filePath)
        {
            try
            {
                byte[] encryptedFileContent = File.ReadAllBytes(filePath);
                ICryptoTransform transform = provider.CreateDecryptor();
                byte[] decrypted = transform.TransformFinalBlock(encryptedFileContent, 0, encryptedFileContent.Length);
                File.WriteAllBytes(filePath, decrypted);
            }
            catch (Exception exc)
            {
                Console.Write("Error:" + exc.Message + "\nFile:" + filePath + "\n");
            }
        }


        public string EncryptPassword(string textToEncrypt, string usrPassword)
        {
            byte[] encryptBytes;
            byte[] passwordBytes = Encoding.Default.GetBytes(textToEncrypt);
            byte[] aesKey = SHA256Managed.Create().ComputeHash(Encoding.Default.GetBytes(usrPassword));

            AES.Key = aesKey;
            AES.Mode = CipherMode.ECB;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(passwordBytes, 0, passwordBytes.Length);
                    }

                    encryptBytes = ms.ToArray();
                }
                string encryptedBase64 = Convert.ToBase64String(encryptBytes);

                return encryptedBase64;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DecryptPassword(string textToDecrypt, string usrPassword)
        {
            byte[] decryptBytes;
            byte[] passwordBytes = Convert.FromBase64String(textToDecrypt);
            byte[] aesKey = SHA256Managed.Create().ComputeHash(Encoding.Default.GetBytes(usrPassword));

            AES.Key = aesKey;
            AES.Mode = CipherMode.ECB;


            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(passwordBytes, 0, passwordBytes.Length);
                }

                decryptBytes = ms.ToArray();
            }
            string decryptedString = Encoding.Default.GetString(decryptBytes);

            return decryptedString;
        }

        public string CreateHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }
    }
}
