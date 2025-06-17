using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanlythuvien
{
    public partial class Chitietphieumuon : Form
    {
        string maPM;
        Connection_SQL connection = new Connection_SQL();

        public Chitietphieumuon(string maPM)
        {
            InitializeComponent();
            this.maPM = maPM;
            txtMaPM.Text = maPM;
            HienThi();
            HienCombo();
            txtMaPM.Enabled= false;
        }

        private void HienCombo()
        {
            DataTable dtTenSach = connection.LayTatCaTenSach();
            cbo_maSach.DataSource = dtTenSach;
            cbo_maSach.DisplayMember = "DisplayText";
            cbo_maSach.ValueMember = "MaSach";
            cbo_maSach.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbo_maSach.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void HienThi()
        {
            DataTable dtpm = connection.LayChiTietPhieuMuon(maPM);
            dataGridView1.DataSource = dtpm;
        }
        bool KtRong()
        {
            if (txtMaPM.Text == "") return false;
            if (cbo_maSach.Text == "") return false;
            if (cbTinhTrang.Text == "") return false;
            return true;
        }

        void resetThongTin()
        {
            txtMaPM.Text = "";
            cbo_maSach.Text = "";
            cbTinhTrang.Text = "";
            txtMaPM.Focus();
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                string maPM = txtMaPM.Text;
                string maSach = cbo_maSach.SelectedValue.ToString();
                string tinhTrangKhiMuon = cbTinhTrang.Text;

                if (string.IsNullOrEmpty(maPM) || string.IsNullOrEmpty(maSach))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin.");
                    return;
                }
                int soSachDaMuon = connection.DemSoSachMuon(maPM);
                if (soSachDaMuon >= 3)
                {
                    MessageBox.Show("Độc giả đã mượn tối đa 3 quyển sách. Không thể mượn thêm.");
                    return;
                }
                connection.ThemChiTietPhieuMuon(maPM, maSach, tinhTrangKhiMuon);

                MessageBox.Show("Thêm chi tiết phiếu mượn thành công!");
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm chi tiết phiếu mượn: " + ex.Message);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                string maPM = txtMaPM.Text;
                string maSach = cbo_maSach.SelectedValue.ToString();  
                string tinhTrangKhiMuon = cbTinhTrang.Text;

                if (string.IsNullOrEmpty(maPM) || string.IsNullOrEmpty(maSach))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin.");
                    return;
                }

                connection.SuaChiTietPhieuMuon(maPM, maSach, tinhTrangKhiMuon);

                MessageBox.Show("Sửa chi tiết phiếu mượn thành công!");
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa chi tiết phiếu mượn: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    txtMaPM.Text = dataGridView1.Rows[e.RowIndex].Cells["MaPM"].Value.ToString();
                    cbo_maSach.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["MaSach"].Value.ToString();
                    cbTinhTrang.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["TinhTrangKhiMuon"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maPM = txtMaPM.Text;
                string maSach = cbo_maSach.SelectedValue.ToString();

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connection.XoaChiTietPhieuMuon(maPM, maSach);
                    MessageBox.Show("Xóa OK!");
                    HienThi();
                }
            }
                catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi xóa : " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa : " + ex.Message);
            }

        }
    }
}
