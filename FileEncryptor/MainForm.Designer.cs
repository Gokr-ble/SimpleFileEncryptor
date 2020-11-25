namespace FileEncryptor
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.SrcFileTextBox = new System.Windows.Forms.TextBox();
            this.EncFileBtn = new System.Windows.Forms.Button();
            this.DecFileBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.FileChooseBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.KeyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "源文件";
            // 
            // SrcFileTextBox
            // 
            this.SrcFileTextBox.AllowDrop = true;
            this.SrcFileTextBox.Location = new System.Drawing.Point(70, 19);
            this.SrcFileTextBox.Name = "SrcFileTextBox";
            this.SrcFileTextBox.Size = new System.Drawing.Size(300, 25);
            this.SrcFileTextBox.TabIndex = 0;
            // 
            // EncFileBtn
            // 
            this.EncFileBtn.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EncFileBtn.Location = new System.Drawing.Point(70, 116);
            this.EncFileBtn.Name = "EncFileBtn";
            this.EncFileBtn.Size = new System.Drawing.Size(145, 30);
            this.EncFileBtn.TabIndex = 3;
            this.EncFileBtn.Text = "文件加密";
            this.EncFileBtn.UseVisualStyleBackColor = true;
            this.EncFileBtn.Click += new System.EventHandler(this.EncFileBtn_Click);
            // 
            // DecFileBtn
            // 
            this.DecFileBtn.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DecFileBtn.Location = new System.Drawing.Point(221, 116);
            this.DecFileBtn.Name = "DecFileBtn";
            this.DecFileBtn.Size = new System.Drawing.Size(145, 30);
            this.DecFileBtn.TabIndex = 4;
            this.DecFileBtn.Text = "文件解密";
            this.DecFileBtn.UseVisualStyleBackColor = true;
            this.DecFileBtn.Click += new System.EventHandler(this.DecFileBtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(11, 168);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(410, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // FileChooseBtn
            // 
            this.FileChooseBtn.Location = new System.Drawing.Point(376, 17);
            this.FileChooseBtn.Name = "FileChooseBtn";
            this.FileChooseBtn.Size = new System.Drawing.Size(45, 25);
            this.FileChooseBtn.TabIndex = 1;
            this.FileChooseBtn.Text = "...";
            this.FileChooseBtn.UseVisualStyleBackColor = true;
            this.FileChooseBtn.Click += new System.EventHandler(this.FileChooseBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(24, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "密码";
            // 
            // KeyTextBox
            // 
            this.KeyTextBox.Location = new System.Drawing.Point(70, 59);
            this.KeyTextBox.MaxLength = 32;
            this.KeyTextBox.Name = "KeyTextBox";
            this.KeyTextBox.Size = new System.Drawing.Size(300, 25);
            this.KeyTextBox.TabIndex = 2;
            this.KeyTextBox.UseSystemPasswordChar = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 203);
            this.Controls.Add(this.KeyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FileChooseBtn);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.DecFileBtn);
            this.Controls.Add(this.EncFileBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SrcFileTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "AES加密器";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SrcFileTextBox;
        private System.Windows.Forms.Button EncFileBtn;
        private System.Windows.Forms.Button DecFileBtn;
        private System.Windows.Forms.Button FileChooseBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KeyTextBox;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

