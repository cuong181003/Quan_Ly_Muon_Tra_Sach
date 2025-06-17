using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlythuvien
{
    public partial class Thongtincanhan : Form
    {
        private string maNV;
        private Connection_SQL connection = new Connection_SQL();
        public Thongtincanhan(string maNVLogin)
        {
            InitializeComponent();
            maNV = maNVLogin;
            loadttcn();
            txtHoTen.Enabled = false;
            txtSDT.Enabled = false;
            txtEmail.Enabled = false;
            dtpNgaySinh.Enabled = false;
            rdoNam.Enabled = false;
            rdoNu.Enabled = false;
            txtMatKhauCu.Visible = false;
            txtMatKhauMoi.Visible = false;
            txtNhapLaiMatKhauMoi.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;

        }

        private void loadttcn()
    {
        DataTable dt = connection.LayThongTinCaNhan(maNV);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtHoTen.Text = row["HoTenNV"].ToString();
                DateTime ngaySinh = Convert.ToDateTime(row["NgaySinh"]);
                dtpNgaySinh.Value = ngaySinh;
                txtSDT.Text = row["SDT"].ToString();
                txtEmail.Text = row["Email"].ToString();

                string gioiTinh = row["GioiTinh"].ToString();
                if (gioiTinh == "Nam")
                {
                    rdoNam.Checked = true;
                }
                else if (gioiTinh == "Nữ")
                {
                    rdoNu.Checked = true;
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên.");
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (btn_sua.Text == "Sửa Thông Tin")
            {
                txtHoTen.Enabled = true;
                txtSDT.Enabled = true;
                txtEmail.Enabled = true;
                dtpNgaySinh.Enabled = true;
                rdoNam.Enabled = true;
                rdoNu.Enabled = true;

                btn_sua.Text = "Lưu Thông Tin"; 
            }
            else
            {
                string hoTen = txtHoTen.Text;
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                DateTime ngaySinh = dtpNgaySinh.Value;
                string gioiTinh = rdoNam.Checked ? "Nam" : "Nữ";

                bool success = connection.SuaNV(maNV, hoTen, ngaySinh, gioiTinh, sdt, email);
                if (success)
                {
                    MessageBox.Show("Thông tin đã được cập nhật thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                btn_sua.Text = "Sửa Thông Tin";

                txtHoTen.Enabled = false;
                txtSDT.Enabled = false;
                txtEmail.Enabled = false;
                dtpNgaySinh.Enabled = false;
                rdoNam.Enabled = false;
                rdoNu.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Đổi mật khẩu")
            {
                txtMatKhauCu.Visible = true;
                txtMatKhauMoi.Visible = true;
                txtNhapLaiMatKhauMoi.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                button1.Text = "Cập nhật mật khẩu";
            }
            else
            {
                string matKhauCu = txtMatKhauCu.Text;
                string matKhauMoi = txtMatKhauMoi.Text;
                string nhapLaiMatKhauMoi = txtNhapLaiMatKhauMoi.Text;

                if (matKhauMoi != nhapLaiMatKhauMoi)
                {
                    MessageBox.Show("Mật khẩu mới không khớp. Vui lòng nhập lại.");
                    return;
                }

                if (connection.KiemTraMatKhauCu(maNV, matKhauCu))
                {
                    if (connection.CapNhatMatKhau(maNV, matKhauMoi))
                    {
                        MessageBox.Show("Đổi mật khẩu thành công!");
                        button1.Text = "Đổi mật khẩu";
                        txtMatKhauCu.Visible = false;
                        txtMatKhauMoi.Visible = false;
                        txtNhapLaiMatKhauMoi.Visible = false;
                        label6.Visible = false;
                        label7.Visible = false;
                        label8.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu thất bại.");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu hiện tại không đúng.");
                }
            }
        }
    }
}
