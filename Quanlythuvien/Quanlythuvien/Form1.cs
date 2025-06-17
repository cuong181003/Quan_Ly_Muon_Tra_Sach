using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlythuvien
{
    public partial class Form1 : Form
    {
        Connection_SQL connection = new Connection_SQL();

        public Form1()
        {
            InitializeComponent();
            txtUsername.Focus();
            txtPass.UseSystemPasswordChar = true;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string maNV = txtUsername.Text;
            string matKhau = txtPass.Text;

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã nhân viên và mật khẩu.");
                return;
            }

            bool isKiemTraLogin = connection.KiemTraDangNhap(maNV, matKhau);

            if (isKiemTraLogin)
            {
                MessageBox.Show("Đăng nhập thành công!");
                Dashbroad mainForm = new Dashbroad(maNV);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Mã nhân viên hoặc mật khẩu không chính xác.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (txtPass.UseSystemPasswordChar)  
            {
                txtPass.UseSystemPasswordChar = false;  
            }
            else 
            {
                txtPass.UseSystemPasswordChar = true;  
            }
        }
    }
}
