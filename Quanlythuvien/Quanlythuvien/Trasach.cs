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
    public partial class Trasach : Form
    {
        Connection_SQL connection = new Connection_SQL();
        string selectedMaPM; string selectedMaSach;
        public Trasach()
        {
            InitializeComponent(); HienThiThongTinTraSach();
        }
        private void HienThiThongTinTraSach()
        { 
            DataTable dtMuonSach = connection.LayThongTinTraSach();
            dataGridView1.DataSource = dtMuonSach;
         }

        private void btn_traSach_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedMaPM) && !string.IsNullOrEmpty(selectedMaSach))
            {
                try
                {
                    bool trangThai = connection.LayTrangThaiPhieuMuon(selectedMaPM, selectedMaSach);

                    if (trangThai)
                    {
                        MessageBox.Show("Quyển sách này đã được trả trước đó.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (txtTinhTrangTra.Text == null)
                    {
                        MessageBox.Show("VUi lòng nhập trạng thái sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!string.IsNullOrEmpty(txtTinhTrangTra.Text))
                    {
                        string trangThaiTra = txtTinhTrangTra.Text;
                        connection.CapNhatTrangThaiTraSach(selectedMaPM, selectedMaSach, trangThaiTra);
                        MessageBox.Show("Cập nhật trạng thái trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HienThiThongTinTraSach();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn tình trạng trả sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật trạng thái trả sách: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để cập nhật trạng thái trả sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    selectedMaPM = dataGridView1.Rows[e.RowIndex].Cells["MaPM"].Value.ToString();
                    selectedMaSach = dataGridView1.Rows[e.RowIndex].Cells["MaSach"].Value.ToString(); 

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string maPhieuMuon = txt_maPM.Text.Trim();

            if (string.IsNullOrEmpty(maPhieuMuon))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu mượn để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connection.MoKetNoi();
                DataTable dt = connection.TimKiemPhieuTra(maPhieuMuon);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phiếu mượn với mã đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                connection.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm phiếu mượn: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HienThiThongTinTraSach();
            txt_maPM.Text = "";
        }
    }
    
}
