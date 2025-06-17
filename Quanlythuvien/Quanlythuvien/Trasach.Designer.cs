namespace Quanlythuvien
{
    partial class Trasach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trasach));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_maPM = new System.Windows.Forms.TextBox();
            this.lbl_maPM = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_functions = new System.Windows.Forms.GroupBox();
            this.txtTinhTrangTra = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_traSach = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox_functions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(35, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(643, 316);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(672, 316);
            this.panel1.TabIndex = 1;
            // 
            // txt_maPM
            // 
            this.txt_maPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_maPM.Location = new System.Drawing.Point(168, 45);
            this.txt_maPM.Name = "txt_maPM";
            this.txt_maPM.Size = new System.Drawing.Size(261, 27);
            this.txt_maPM.TabIndex = 36;
            // 
            // lbl_maPM
            // 
            this.lbl_maPM.AutoSize = true;
            this.lbl_maPM.BackColor = System.Drawing.Color.White;
            this.lbl_maPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_maPM.Location = new System.Drawing.Point(6, 48);
            this.lbl_maPM.Name = "lbl_maPM";
            this.lbl_maPM.Size = new System.Drawing.Size(128, 20);
            this.lbl_maPM.TabIndex = 35;
            this.lbl_maPM.Text = "Mã phiếu mượn ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 341);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1277, 347);
            this.dataGridView1.TabIndex = 49;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(1052, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 41);
            this.label1.TabIndex = 50;
            this.label1.Text = "Trả Sách";
            // 
            // groupBox_functions
            // 
            this.groupBox_functions.Controls.Add(this.txtTinhTrangTra);
            this.groupBox_functions.Controls.Add(this.label2);
            this.groupBox_functions.Controls.Add(this.button2);
            this.groupBox_functions.Controls.Add(this.button1);
            this.groupBox_functions.Controls.Add(this.btn_traSach);
            this.groupBox_functions.Controls.Add(this.txt_maPM);
            this.groupBox_functions.Controls.Add(this.lbl_maPM);
            this.groupBox_functions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_functions.Location = new System.Drawing.Point(827, 63);
            this.groupBox_functions.Name = "groupBox_functions";
            this.groupBox_functions.Size = new System.Drawing.Size(462, 272);
            this.groupBox_functions.TabIndex = 51;
            this.groupBox_functions.TabStop = false;
            this.groupBox_functions.Text = "Chức năng";
            // 
            // txtTinhTrangTra
            // 
            this.txtTinhTrangTra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTinhTrangTra.Location = new System.Drawing.Point(168, 163);
            this.txtTinhTrangTra.Name = "txtTinhTrangTra";
            this.txtTinhTrangTra.Size = new System.Drawing.Size(261, 27);
            this.txtTinhTrangTra.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 37;
            this.label2.Text = "Trạng thái trả";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MintCream;
            this.button2.Location = new System.Drawing.Point(255, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 35);
            this.button2.TabIndex = 11;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MintCream;
            this.button1.Location = new System.Drawing.Point(72, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 35);
            this.button1.TabIndex = 10;
            this.button1.Text = "Tìm kiếm";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_traSach
            // 
            this.btn_traSach.BackColor = System.Drawing.Color.OliveDrab;
            this.btn_traSach.Location = new System.Drawing.Point(147, 214);
            this.btn_traSach.Name = "btn_traSach";
            this.btn_traSach.Size = new System.Drawing.Size(122, 41);
            this.btn_traSach.TabIndex = 9;
            this.btn_traSach.Text = "Trả sách";
            this.btn_traSach.UseVisualStyleBackColor = false;
            this.btn_traSach.Click += new System.EventHandler(this.btn_traSach_Click);
            // 
            // Trasach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(1301, 700);
            this.Controls.Add(this.groupBox_functions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Trasach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trả sách";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox_functions.ResumeLayout(false);
            this.groupBox_functions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_maPM;
        private System.Windows.Forms.Label lbl_maPM;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_functions;
        private System.Windows.Forms.Button btn_traSach;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTinhTrangTra;
    }
}