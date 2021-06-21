using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace FileEncryptor
{
    class FileOperator
    {
        public delegate void UpdateUI();
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask(string savePath, int type);
        public AccomplishTask CallBackDelegate;

        public delegate void WrongPassword();
        public WrongPassword wrongPassword;

        private string filePath;
        private string userKey;

        public FileOperator(string p, string k)
        {
            filePath = p;
            userKey = k;
        }

        public void EncryptFile()
        {
            //源文件位置
            string pathSource = filePath;

            //加密文件位置
            string pathTarget = pathSource + ".myenc";
            //密钥生成，补全32位
            Encryptor encryptor = new Encryptor();
            //string key = encryptor.KeyGenerate(userKey);
            byte[] keyBytes = encryptor.KeyGenerate(userKey);

            //获取密钥Hash值
            //byte[] hash = new HashUtil().MD5Encrypt(key);

            FileStream inputFile = new FileStream(pathSource, FileMode.Open);
            FileStream outputFile = new FileStream(pathTarget, FileMode.Create, FileAccess.Write);

            //文件读取缓冲区
            byte[] buf = new byte[1024];

            //写入密钥Hash值
            outputFile.Write(keyBytes, 0, 16);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (true)
            {
                int len = inputFile.Read(buf, 0, buf.Length);
                if (len > 0)
                {
                    byte[] c_part = encryptor.Encrypt(buf, keyBytes);
                    outputFile.Write(c_part, 0, len);
                }
                else
                {
                    break;
                }
                UpdateUIDelegate();
            }
            inputFile.Close();
            outputFile.Close();

            sw.Stop();
            System.Windows.Forms.MessageBox.Show(sw.ElapsedMilliseconds.ToString());
            // Console.WriteLine(sw.ElapsedMilliseconds);

            CallBackDelegate(pathTarget, 0);
        }

        public void DecryptFile()
        {
            //源文件位置
            string pathSource = filePath;

            //解密文件位置
            string pathOrigin = Path.GetFileNameWithoutExtension(pathSource);           //原始文件名和扩展名
            string pathName = Path.GetDirectoryName(pathSource);                        //文件路径
            string pathOriNoExt = Path.GetFileNameWithoutExtension(pathOrigin);         //原始文件名
            string pathExtention = Path.GetExtension(pathOrigin);                       //原始扩展名
            string pathTarget = Path.Combine(pathName, pathOriNoExt + "_dec" + pathExtention);    //解密拼接后的文件名

            //密钥生成补全32位、获取密钥Hash值
            Encryptor encryptor = new Encryptor();
            //string key = encryptor.KeyGenerate(userKey);
            byte[] keyBytes = encryptor.KeyGenerate(userKey);

            //byte[] hash = new HashUtil().MD5Encrypt(key);

            FileStream inputFile = new FileStream(pathSource, FileMode.Open);            

            //文件读取缓冲区
            byte[] buf = new byte[1024];

            //读取文件开头16字节的Hash值并进行对比
            byte[] hash_test = new byte[16];
            inputFile.Read(hash_test, 0, 16);

            if (HashCheck(keyBytes, hash_test, 16) == false)
            {
                wrongPassword();
                inputFile.Close();
                return;
            }
            else
            {
                //此处在只有密码验证正确后才打开写入文件流
                FileStream outputFile = new FileStream(pathTarget, FileMode.Create, FileAccess.Write);
                while (true)
                {
                    int len = inputFile.Read(buf, 0, buf.Length);
                    byte[] p_part;
                    if (len > 0)
                    {
                        p_part = encryptor.Decrypt(buf, keyBytes);
                        outputFile.Write(p_part, 0, len);
                    }
                    else
                    {
                        break;
                    }
                    UpdateUIDelegate();
                }
                inputFile.Close();
                outputFile.Close();

                CallBackDelegate(pathTarget, 1);
            }
        }

        private bool HashCheck(byte[] arr1, byte[] arr2, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;
        }
    }
}
