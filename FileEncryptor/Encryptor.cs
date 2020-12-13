using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FileEncryptor
{
    class Encryptor
    {
        public byte[] Encrypt(byte[] array, byte[] key)
        {
            //byte[] keyArray = Encoding.UTF8.GetBytes(key);

            RijndaelManaged rDel = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.None
            };

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(array, 0, array.Length);
            return resultArray;

        }

        public byte[] Decrypt(byte[] array, byte[] key)
        {
            //byte[] keyArray = Encoding.UTF8.GetBytes(key);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = key;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.None;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(array, 0, array.Length);

            return resultArray;
        }

        public byte[] KeyGenerate(string key)
        {
            byte[] keyArray = new HashUtil().MD5Encrypt(key);
            return keyArray;
        }

    }
}
