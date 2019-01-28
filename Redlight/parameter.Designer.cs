namespace Redlight
{
    partial class parameter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(parameter));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.facewidth = new System.Windows.Forms.NumericUpDown();
            this.score = new System.Windows.Forms.NumericUpDown();
            this.FACE_ROLL_0 = new System.Windows.Forms.RadioButton();
            this.FACE_ROLL_90 = new System.Windows.Forms.RadioButton();
            this.FACE_ROLL_180 = new System.Windows.Forms.RadioButton();
            this.FACE_ROLL_270 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.save = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.duankouhao = new System.Windows.Forms.ComboBox();
            this.btl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.wangluo = new System.Windows.Forms.RadioButton();
            this.usbbutton = new System.Windows.Forms.RadioButton();
            this.path = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_tsy = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.facewidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.score)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "识别最小脸宽";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "相似度值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "摄像头角度";
            // 
            // facewidth
            // 
            this.facewidth.Location = new System.Drawing.Point(147, 127);
            this.facewidth.Maximum = new decimal(new int[] {
            86,
            0,
            0,
            0});
            this.facewidth.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.facewidth.Name = "facewidth";
            this.facewidth.Size = new System.Drawing.Size(47, 21);
            this.facewidth.TabIndex = 3;
            this.facewidth.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // score
            // 
            this.score.Location = new System.Drawing.Point(147, 178);
            this.score.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(47, 21);
            this.score.TabIndex = 4;
            // 
            // FACE_ROLL_0
            // 
            this.FACE_ROLL_0.AutoSize = true;
            this.FACE_ROLL_0.Location = new System.Drawing.Point(0, 14);
            this.FACE_ROLL_0.Name = "FACE_ROLL_0";
            this.FACE_ROLL_0.Size = new System.Drawing.Size(125, 16);
            this.FACE_ROLL_0.TabIndex = 5;
            this.FACE_ROLL_0.TabStop = true;
            this.FACE_ROLL_0.Text = "人脸向上(无旋转) ";
            this.FACE_ROLL_0.UseVisualStyleBackColor = true;
            // 
            // FACE_ROLL_90
            // 
            this.FACE_ROLL_90.AutoSize = true;
            this.FACE_ROLL_90.Location = new System.Drawing.Point(132, 14);
            this.FACE_ROLL_90.Name = "FACE_ROLL_90";
            this.FACE_ROLL_90.Size = new System.Drawing.Size(167, 16);
            this.FACE_ROLL_90.TabIndex = 6;
            this.FACE_ROLL_90.TabStop = true;
            this.FACE_ROLL_90.Text = "人脸向右(逆时针旋转90度)";
            this.FACE_ROLL_90.UseVisualStyleBackColor = true;
            // 
            // FACE_ROLL_180
            // 
            this.FACE_ROLL_180.AutoSize = true;
            this.FACE_ROLL_180.Location = new System.Drawing.Point(0, 50);
            this.FACE_ROLL_180.Name = "FACE_ROLL_180";
            this.FACE_ROLL_180.Size = new System.Drawing.Size(107, 16);
            this.FACE_ROLL_180.TabIndex = 7;
            this.FACE_ROLL_180.TabStop = true;
            this.FACE_ROLL_180.Text = "人脸向下(倒置)";
            this.FACE_ROLL_180.UseVisualStyleBackColor = true;
            // 
            // FACE_ROLL_270
            // 
            this.FACE_ROLL_270.AutoSize = true;
            this.FACE_ROLL_270.Location = new System.Drawing.Point(132, 50);
            this.FACE_ROLL_270.Name = "FACE_ROLL_270";
            this.FACE_ROLL_270.Size = new System.Drawing.Size(167, 16);
            this.FACE_ROLL_270.TabIndex = 8;
            this.FACE_ROLL_270.TabStop = true;
            this.FACE_ROLL_270.Text = "人脸向左(顺时针旋转90度)";
            this.FACE_ROLL_270.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FACE_ROLL_90);
            this.panel1.Controls.Add(this.FACE_ROLL_270);
            this.panel1.Controls.Add(this.FACE_ROLL_0);
            this.panel1.Controls.Add(this.FACE_ROLL_180);
            this.panel1.Location = new System.Drawing.Point(147, 227);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 80);
            this.panel1.TabIndex = 9;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(177, 517);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 10;
            this.save.Text = "保存";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(365, 517);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 11;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "端口号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 399);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "波特率";
            // 
            // duankouhao
            // 
            this.duankouhao.FormattingEnabled = true;
            this.duankouhao.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13"});
            this.duankouhao.Location = new System.Drawing.Point(147, 342);
            this.duankouhao.Name = "duankouhao";
            this.duankouhao.Size = new System.Drawing.Size(83, 20);
            this.duankouhao.TabIndex = 14;
            // 
            // btl
            // 
            this.btl.Location = new System.Drawing.Point(147, 390);
            this.btl.Name = "btl";
            this.btl.Size = new System.Drawing.Size(100, 21);
            this.btl.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "摄像头选项";
            // 
            // wangluo
            // 
            this.wangluo.AutoSize = true;
            this.wangluo.Location = new System.Drawing.Point(0, 19);
            this.wangluo.Name = "wangluo";
            this.wangluo.Size = new System.Drawing.Size(83, 16);
            this.wangluo.TabIndex = 17;
            this.wangluo.TabStop = true;
            this.wangluo.Text = "网络摄像头";
            this.wangluo.UseVisualStyleBackColor = true;
            // 
            // usbbutton
            // 
            this.usbbutton.AutoSize = true;
            this.usbbutton.Location = new System.Drawing.Point(119, 19);
            this.usbbutton.Name = "usbbutton";
            this.usbbutton.Size = new System.Drawing.Size(77, 16);
            this.usbbutton.TabIndex = 18;
            this.usbbutton.TabStop = true;
            this.usbbutton.Text = "USB摄像头";
            this.usbbutton.UseVisualStyleBackColor = true;
            this.usbbutton.CheckedChanged += new System.EventHandler(this.usbbutton_CheckedChanged);
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(147, 75);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(279, 21);
            this.path.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.usbbutton);
            this.panel2.Controls.Add(this.wangluo);
            this.panel2.Location = new System.Drawing.Point(147, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 57);
            this.panel2.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 445);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "提示语";
            // 
            // textBox_tsy
            // 
            this.textBox_tsy.Location = new System.Drawing.Point(147, 442);
            this.textBox_tsy.Name = "textBox_tsy";
            this.textBox_tsy.Size = new System.Drawing.Size(279, 21);
            this.textBox_tsy.TabIndex = 22;
            // 
            // parameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 580);
            this.Controls.Add(this.textBox_tsy);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.path);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btl);
            this.Controls.Add(this.duankouhao);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.close);
            this.Controls.Add(this.save);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.score);
            this.Controls.Add(this.facewidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "parameter";
            this.Text = "参数设置";
            ((System.ComponentModel.ISupportInitialize)(this.facewidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.score)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown facewidth;
        private System.Windows.Forms.NumericUpDown score;
        private System.Windows.Forms.RadioButton FACE_ROLL_0;
        private System.Windows.Forms.RadioButton FACE_ROLL_90;
        private System.Windows.Forms.RadioButton FACE_ROLL_180;
        private System.Windows.Forms.RadioButton FACE_ROLL_270;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox duankouhao;
        private System.Windows.Forms.TextBox btl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton wangluo;
        private System.Windows.Forms.RadioButton usbbutton;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_tsy;
    }
}