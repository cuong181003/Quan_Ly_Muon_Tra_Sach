namespace Quanlythuvien
{
    partial class Chitietphieumuon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chitietphieumuon));
            this.groupBox_functions = new System.Windows.Forms.GroupBox();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.lbl_muonsach = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbTinhTrang = new System.Windows.Forms.ComboBox();
            this.txtMaPM = new System.Windows.Forms.TextBox();
            this.lbl_maPM = new System.Windows.Forms.Label();
            this.cbo_maSach = new System.Windows.Forms.ComboBox();
            this.lbl_ttkm = new System.Windows.Forms.Label();
            this.lbl_maSach = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox_functions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_functions
            // 
            this.groupBox_functions.BackColor = System.Drawing.Color.PeachPuff;
            this.groupBox_functions.Controls.Add(this.btn_xoa);
            this.groupBox_functions.Controls.Add(this.btn_sua);
            this.groupBox_functions.Controls.Add(this.btn_them);
            this.groupBox_functions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_functions.Location = new System.Drawing.Point(547, 146);
            this.groupBox_functions.Name = "groupBox_functions";
            this.groupBox_functions.Size = new System.Drawing.Size(272, 70);
            this.groupBox_functions.TabIndex = 41;
            this.groupBox_functions.TabStop = false;
            this.groupBox_functions.Text = "Chức năng";
            // 
            // btn_xoa
            // 
            this.btn_xoa.Location = new System.Drawing.Point(185, 26);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(81, 35);
            this.btn_xoa.TabIndex = 4;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.Location = new System.Drawing.Point(93, 26);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(81, 35);
            this.btn_sua.TabIndex = 3;
            this.btn_sua.Text = "Sửa";
            this.btn_sua.UseVisualStyleBackColor = true;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_them
            // 
            this.btn_them.Location = new System.Drawing.Point(6, 26);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(81, 35);
            this.btn_them.TabIndex = 1;
            this.btn_them.Text = "Thêm";
            this.btn_them.UseVisualStyleBackColor = true;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // lbl_muonsach
            // 
            this.lbl_muonsach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_muonsach.BackColor = System.Drawing.Color.Lavender;
            this.lbl_muonsach.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_muonsach.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_muonsach.Location = new System.Drawing.Point(177, 9);
            this.lbl_muonsach.Name = "lbl_muonsach";
            this.lbl_muonsach.Size = new System.Drawing.Size(461, 33);
            this.lbl_muonsach.TabIndex = 40;
            this.lbl_muonsach.Text = "CHI TIẾT PHIẾU MƯỢN";
            this.lbl_muonsach.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.PeachPuff;
            this.groupBox2.Controls.Add(this.cbTinhTrang);
            this.groupBox2.Controls.Add(this.txtMaPM);
            this.groupBox2.Controls.Add(this.lbl_maPM);
            this.groupBox2.Controls.Add(this.cbo_maSach);
            this.groupBox2.Controls.Add(this.lbl_ttkm);
            this.groupBox2.Controls.Add(this.lbl_maSach);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 77);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 139);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            // 
            // cbTinhTrang
            // 
            this.cbTinhTrang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTinhTrang.FormattingEnabled = true;
            this.cbTinhTrang.Items.AddRange(new object[] {
            "Mới",
            "Cũ"});
            this.cbTinhTrang.Location = new System.Drawing.Point(193, 101);
            this.cbTinhTrang.Name = "cbTinhTrang";
            this.cbTinhTrang.Size = new System.Drawing.Size(261, 28);
            this.cbTinhTrang.TabIndex = 38;
            // 
            // txtMaPM
            // 
            this.txtMaPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaPM.Location = new System.Drawing.Point(193, 25);
            this.txtMaPM.Name = "txtMaPM";
            this.txtMaPM.Size = new System.Drawing.Size(260, 27);
            this.txtMaPM.TabIndex = 37;
            // 
            // lbl_maPM
            // 
            this.lbl_maPM.AutoSize = true;
            this.lbl_maPM.BackColor = System.Drawing.Color.White;
            this.lbl_maPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_maPM.Location = new System.Drawing.Point(6, 33);
            this.lbl_maPM.Name = "lbl_maPM";
            this.lbl_maPM.Size = new System.Drawing.Size(128, 20);
            this.lbl_maPM.TabIndex = 36;
            this.lbl_maPM.Text = "Mã phiếu mượn ";
            // 
            // cbo_maSach
            // 
            this.cbo_maSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_maSach.FormattingEnabled = true;
            this.cbo_maSach.Location = new System.Drawing.Point(192, 59);
            this.cbo_maSach.Name = "cbo_maSach";
            this.cbo_maSach.Size = new System.Drawing.Size(261, 28);
            this.cbo_maSach.TabIndex = 35;
            // 
            // lbl_ttkm
            // 
            this.lbl_ttkm.AutoSize = true;
            this.lbl_ttkm.BackColor = System.Drawing.Color.White;
            this.lbl_ttkm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ttkm.Location = new System.Drawing.Point(6, 104);
            this.lbl_ttkm.Name = "lbl_ttkm";
            this.lbl_ttkm.Size = new System.Drawing.Size(156, 20);
            this.lbl_ttkm.TabIndex = 28;
            this.lbl_ttkm.Text = "Tình trạng khi mượn";
            // 
            // lbl_maSach
            // 
            this.lbl_maSach.AutoSize = true;
            this.lbl_maSach.BackColor = System.Drawing.Color.White;
            this.lbl_maSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_maSach.Location = new System.Drawing.Point(6, 66);
            this.lbl_maSach.Name = "lbl_maSach";
            this.lbl_maSach.Size = new System.Drawing.Size(78, 20);
            this.lbl_maSach.TabIndex = 26;
            this.lbl_maSach.Text = "Tên sách";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 244);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(807, 235);
            this.dataGridView1.TabIndex = 42;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Chitietphieumuon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(831, 491);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox_functions);
            this.Controls.Add(this.lbl_muonsach);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Chitietphieumuon";
            this.Text = "Chi tiết phiếu mượn";
            this.groupBox_functions.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_functions;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.Label lbl_muonsach;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_maPM;
        private System.Windows.Forms.Label lbl_ttkm;
        private System.Windows.Forms.Label lbl_maSach;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbo_maSach;
        private System.Windows.Forms.TextBox txtMaPM;
        private System.Windows.Forms.ComboBox cbTinhTrang;
    }
}