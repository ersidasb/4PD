using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _4PD
{
    class Crypto
    {
        private RijndaelManaged AES;

        public Crypto()
        {
            AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;
        }
        private byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                for (int i = 0; i < 10; i++)
                    rng.GetBytes(data);

            return data;
        }

        public void FileEncrypt(string inputFile, string password)
        {
            byte[] salt = GenerateRandomSalt();
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                    cs.Write(buffer, 0, read);

                fsIn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }
        public void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];
            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            fsCrypt.Read(salt, 0, salt.Length);

            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);
            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                    fsOut.Write(buffer, 0, read);

                cs.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
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
