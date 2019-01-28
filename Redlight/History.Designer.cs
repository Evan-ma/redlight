namespace Redlight
{
    partial class History
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtTime = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.hour_box = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.face = new System.Windows.Forms.DataGridViewImageColumn();
            this.panorama1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panorama2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panorama3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dtTime
            // 
            this.dtTime.CustomFormat = "yyyy-MM-dd";
            this.dtTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTime.Location = new System.Drawing.Point(166, 25);
            this.dtTime.Name = "dtTime";
            this.dtTime.Size = new System.Drawing.Size(126, 26);
            this.dtTime.TabIndex = 0;
            this.dtTime.Value = new System.DateTime(2017, 11, 21, 0, 0, 0, 0);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(683, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查找";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // hour_box
            // 
            this.hour_box.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hour_box.FormattingEnabled = true;
            this.hour_box.Items.AddRange(new object[] {
            "(全天)",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.hour_box.Location = new System.Drawing.Point(525, 27);
            this.hour_box.Name = "hour_box";
            this.hour_box.Size = new System.Drawing.Size(76, 24);
            this.hour_box.TabIndex = 3;
            this.hour_box.Text = "(全天)";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.face,
            this.panorama1,
            this.panorama2,
            this.panorama3,
            this.gender,
            this.age,
            this.time});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Location = new System.Drawing.Point(35, 78);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(666, 405);
            this.dataGridView.TabIndex = 18;
            this.dataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentDoubleClick);
            // 
            // face
            // 
            this.face.HeaderText = "人脸";
            this.face.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.face.Name = "face";
            this.face.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.face.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panorama1
            // 
            this.panorama1.HeaderText = "全景1";
            this.panorama1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.panorama1.MinimumWidth = 100;
            this.panorama1.Name = "panorama1";
            this.panorama1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.panorama1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panorama2
            // 
            this.panorama2.HeaderText = "全景2";
            this.panorama2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.panorama2.Name = "panorama2";
            this.panorama2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.panorama2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panorama3
            // 
            this.panorama3.HeaderText = "全景3";
            this.panorama3.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.panorama3.Name = "panorama3";
            this.panorama3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.panorama3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // gender
            // 
            this.gender.HeaderText = "性别";
            this.gender.Name = "gender";
            this.gender.Width = 80;
            // 
            // age
            // 
            this.age.HeaderText = "年龄";
            this.age.Name = "age";
            this.age.Width = 80;
            // 
            // time
            // 
            this.time.HeaderText = "时间";
            this.time.Name = "time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(32, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "选择日期:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(337, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "选择时间(小时):";
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 509);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.hour_box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtTime);
            this.Name = "History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox hour_box;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewImageColumn face;
        private System.Windows.Forms.DataGridViewImageColumn panorama1;
        private System.Windows.Forms.DataGridViewImageColumn panorama2;
        private System.Windows.Forms.DataGridViewImageColumn panorama3;
        private System.Windows.Forms.DataGridViewTextBoxColumn gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn age;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}