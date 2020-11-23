using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Timers;

namespace FileEncryptor
{
    public partial class Form1 : Form
    {
        private Encryptor encryptor = new Encryptor();
        public Form1()
        {
            InitializeComponent();
        }       

        private void EncryptFile()
        {
            //源文件位置
            string pathSource = SrcFileTextBox.Text;
            //加密文件位置
            string pathTarget = pathSource + ".myenc";
            //密钥生成
            string key = encryptor.KeyGen();

            FileStream inputFile = new FileStream(pathSource, FileMode.Open);
            FileStream outputFile = new FileStream(pathTarget, FileMode.Create, FileAccess.Write);

            byte[] buf = new byte[1024];

            ProgressBarInit((int)new FileInfo(pathSource).Length, 1024);

            while (true)
            {
                int len = inputFile.Read(buf, 0, buf.Length);
                byte[] c_part;
                if (len > 0)
                {
                    c_part = encryptor.Encrypt(buf, key);
                    outputFile.Write(c_part, 0, len);
                }
                else
                {
                    break;
                }
                startProgress();
            }

            inputFile.Close();
            outputFile.Close();

            string msg = "文件存储位置：" + pathTarget;
            MessageBox.Show(msg, "加密成功");
        }

        private void DecryptFile()
        {
            //源文件位置
            string pathSource = SrcFileTextBox.Text;
            //解密文件位置
            string pathOrigin = Path.GetFileNameWithoutExtension(pathSource);           //原始文件名和扩展名
            string pathName = Path.GetDirectoryName(pathSource);                        //文件路径
            string pathOriNoExt = Path.GetFileNameWithoutExtension(pathOrigin);         //原始文件名
            string pathExtention = Path.GetExtension(pathOrigin);                       //原始扩展名
            string pathTarget = Path.Combine(pathName, pathOriNoExt + "_dec" + pathExtention);    //解密拼接后的文件名

            string key = encryptor.KeyGen();

            FileStream inputFile = new FileStream(pathSource, FileMode.Open);
            FileStream outputFile = new FileStream(pathTarget, FileMode.Create, FileAccess.Write);

            byte[] buf = new byte[1024];

            ProgressBarInit((int)new FileInfo(pathSource).Length, 1024);

            while (true)
            {
                int len = inputFile.Read(buf, 0, buf.Length);
                byte[] p_part;
                if(len > 0)
                {
                    p_part = encryptor.Decrypt(buf, key);
                    outputFile.Write(p_part, 0, len);
                }
                else
                {
                    break;
                }
                startProgress();
            }

            inputFile.Close();
            outputFile.Close();

            string msg = "文件存储位置：" + pathTarget;
            MessageBox.Show(msg, "解密成功");
        }       

        private void EncFileBtn_Click(object sender, EventArgs e)
        {
            string path = SrcFileTextBox.Text;
            if (path == "")
            {
                MessageBox.Show("请选择正确的文件路径！", "错误", MessageBoxButtons.OK);
            }
            else
            {
                EncryptFile();
            }
            
        }

        private void DecFileBtn_Click(object sender, EventArgs e)
        {
            string path = SrcFileTextBox.Text;
            string ext = Path.GetExtension(path);
            if (path == "" || ext != ".myenc")
            {
                MessageBox.Show("请选择正确的文件路径！", "错误", MessageBoxButtons.OK);
            }
            else
            {
                DecryptFile();
            }
            
        }

        private void FileChooseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            
            fileDialog.Title = "选择文件";
            fileDialog.Filter = "All Files(*.*)|*.*|myenc加密文件(*.myenc)|*.myenc";
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                SrcFileTextBox.Text = fileDialog.FileName;
            }
        }

        private void ProgressBarInit(int MaxLen, int step)
        {
            progressBar1.Value = 1;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = MaxLen;
            progressBar1.Step = step;
        }
        
        private void startProgress()
        {
            progressBar1.PerformStep();
        }
    }
}
