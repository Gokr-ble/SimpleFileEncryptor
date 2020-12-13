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
using System.Security.Principal;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace FileEncryptor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        public MainForm(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0)
            {
                SrcFileTextBox.Text = args[0];
            }
        }

        private void EncFileBtn_Click(object sender, EventArgs e)
        {
            string path = SrcFileTextBox.Text;
            string ext = Path.GetExtension(path);
            string userKey = KeyTextBox.Text;
            //不想重复加密，嗯
            if (path == "" || ext == ".myenc")
            {
                MessageBox.Show("请选择正确的文件路径！", "错误", MessageBoxButtons.OK);
            }
            else if(userKey == "")
            {
                MessageBox.Show("请输入密码！", "错误", MessageBoxButtons.OK);
            }
            else
            {
                FileOperator foperator = new FileOperator(path, userKey);
                ProgressBarInit((int)new FileInfo(path).Length, 1024);

                foperator.UpdateUIDelegate += startProgress;
                foperator.CallBackDelegate += ShowMessage;
                foperator.wrongPassword += WrongPasswordMessage;
                foperator.EncryptFile();
                
            }
    
        }

        private void DecFileBtn_Click(object sender, EventArgs e)
        {
            string path = SrcFileTextBox.Text;
            string ext = Path.GetExtension(path);
            string userKey = KeyTextBox.Text;
            if (path == "" || ext != ".myenc")
            {
                MessageBox.Show("请选择正确的文件路径！", "错误", MessageBoxButtons.OK);
            }
            else if (userKey == "")
            {
                MessageBox.Show("请输入密码！", "错误", MessageBoxButtons.OK);
            }
            else
            {
                FileOperator foperator = new FileOperator(path, userKey);
                ProgressBarInit((int)new FileInfo(path).Length, 1024);
                foperator.UpdateUIDelegate += startProgress;
                foperator.CallBackDelegate += ShowMessage;
                foperator.wrongPassword += WrongPasswordMessage;
                foperator.DecryptFile();
            }
            
        }

        private void FileChooseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            
            fileDialog.Title = "选择文件";
            fileDialog.Filter = "All Files(*.*)|*.*";
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
            //progressBar1.BeginInvoke(new EventHandler((sender, e) =>
            //{
            //    progressBar1.PerformStep();
            //}), null);
        }

        private void ShowMessage(string savePath, int type)
        {
            if (type == 0)
            {
                MessageBox.Show("文件存储位置：" + savePath, "加密成功");
            }
            else
            {
                MessageBox.Show("文件存储位置：" + savePath, "解密成功");
            }
        }

        private void WrongPasswordMessage()
        {
            MessageBox.Show("密码错误！", "Error");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RegistryKey rkClassRoot = Registry.ClassesRoot;
            if (rkClassRoot.OpenSubKey(".myenc") == null)
            {
                if (MessageBox.Show("文件类型未关联，是否建立文件关联？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RunElevated(Application.ExecutablePath);
                    rkClassRoot.Close();

                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                    {
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\.myenc", "", "MyencFile");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\MyencFile", "", "myenc加密文件");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\MyencFile\DefaultIcon", "", @"C:\Program Files\FileEncryptor\folder_key.ico");
                        Registry.SetValue(@"HKEY_CLASSES_ROOT\MyencFile\shell\open\command", "", "C:\\Program Files\\FileEncryptor\\FileEncryptor.exe \"%1\"");
                        MessageBox.Show("文件关联成功！", "确认");
                    }                    
                    this.Close();                                       
                }
            }
        }

        private void RunElevated(string fileName)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.FileName = fileName;
            try
            {
                Process.Start(processInfo);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("文件关联已终止，但软件仍可使用。", "确认");
            }
        }
    }

    public class FileParamPair
    {
        public string filePath;
        public string userKey;
        public FileParamPair(string p, string k)
        {
            filePath = p;
            userKey = k;
        }
    }
}
