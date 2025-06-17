using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Quanlythuvien
{
    public partial class Muonsach : Form
    {
        Connection_SQL connection = new Connection_SQL();
        string manvLogin;

        public Muonsach(string maNV)
        {
            InitializeComponent();
            manvLogin = maNV;
            txtMaNV.Text = manvLogin;
            HienThiThongTinMuonSach();

        }

        private void HienThiThongTinMuonSach()
        {
                DataTable dtMuonSach = connection.LayThongTinMuonSach();
                dataGridView1.DataSource = dtMuonSach;

        }

        private bool KtRong()
        {
            if (string.IsNullOrWhiteSpace(txt_maPM.Text) ||
                string.IsNullOrWhiteSpace(txtTheDG.Text) ||
                string.IsNullOrWhiteSpace(txtMaNV.Text) ||
               
                string.IsNullOrWhiteSpace(dtNgayHenTra.Text))
            {
                return false;
            }
            return true;
        }

        private void resetThongTin()
        {
            txt_maPM.Clear();
            txtTheDG.Clear();
            txtMaNV.Text = manvLogin;
            dtNgayHenTra.Value = DateTime.Now;
            txt_maPM.Focus();
        }

        private void btn_ChiTietPM_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string maPM = dataGridView1.SelectedRows[0].Cells["MaPM"].Value.ToString();
                    DateTime ngayMuon = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["NgayMuon"].Value);

                    if ((DateTime.Now - ngayMuon).TotalDays > 1)
                    {
                        MessageBox.Show("Không thể thêm chi tiết phiếu mượn cho phiếu mượn quá 1 ngày.");
                        return;
                    }

                    Chitietphieumuon ctpm = new Chitietphieumuon(maPM);
                    ctpm.Show();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một phiếu mượn để xem chi tiết.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở chi tiết phiếu mượn: " + ex.Message);
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KtRong())
                {
                    MessageBox.Show("Chưa nhập đủ thông tin!", "Thông báo");
                    return;
                }

                int maPM = int.Parse(txt_maPM.Text.Trim());
                int soTheDG = int.Parse(txtTheDG.Text.Trim());
                DateTime ngayMuon =  DateTime.Now;
                DateTime ngayHenTra = dtNgayHenTra.Value;

                DateTime kiemtrahan = connection.LayNgayHetHanThe(soTheDG); 
                if (kiemtrahan < DateTime.Now)
                {
                    MessageBox.Show("Thẻ độc giả đã hết hạn. Không thể mượn sách.", "Lỗi");
                    return;
                }

                if (connection.KiemTraPhieuMuonTonTai(maPM))
                {
                    MessageBox.Show("Mã phiếu mượn đã tồn tại. Vui lòng nhập mã khác.", "Lỗi");
                    return;
                }

                bool kiemtramuon = connection.KiemTraDocGiaDangMuonHoacQuaHan(soTheDG); 
                if (kiemtramuon)
                {
                    MessageBox.Show("Thẻ độc giả đang mượn sách hoặc bị quá hạn. Không thể mượn sách mới.", "Lỗi");
                    return;
                }


                if (ngayHenTra <= ngayMuon)
                {
                    MessageBox.Show("Ngày hẹn trả phải lớn hơn ngày mượn.", "Lỗi nhập liệu");
                    return;
                }

                connection.ThemPhieuMuon(maPM, soTheDG, manvLogin, ngayMuon, ngayHenTra);
                MessageBox.Show("Thêm phiếu mượn thành công!");
                HienThiThongTinMuonSach();
                resetThongTin();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phiếu mượn: " + ex.Message);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                int maPM = int.Parse(txt_maPM.Text.Trim());
                int soTheDG = int.Parse(txtTheDG.Text.Trim());
                int maNV = int.Parse(txtMaNV.Text.Trim());
                DateTime ngayMuon = connection.LayNgayMuon(maPM);

                DateTime ngayHenTra = dtNgayHenTra.Value;

                if (ngayHenTra <= ngayMuon)
                {
                    MessageBox.Show("Ngày hẹn trả phải lớn hơn ngày mượn.", "Lỗi nhập liệu");
                    return;
                }

                connection.SuaPhieuMuon(maPM, soTheDG, maNV, ngayHenTra);
                MessageBox.Show("Sửa phiếu mượn thành công!");
                HienThiThongTinMuonSach();
                resetThongTin();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa phiếu mượn: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                int maPM = int.Parse(txt_maPM.Text.Trim());

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    connection.XoaPhieuMuon(maPM);
                    MessageBox.Show("Xóa phiếu mượn thành công!");
                    HienThiThongTinMuonSach();
                    resetThongTin();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi xóa phiếu mượn: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phiếu mượn: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    txt_maPM.Text = row.Cells["MaPM"].Value.ToString();
                    txtTheDG.Text = row.Cells["SoTheDG"].Value.ToString();
                    txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
               //     dtp_ngaymuon.Value = Convert.ToDateTime(row.Cells["NgayMuon"].Value);
                    dtNgayHenTra.Value = Convert.ToDateTime(row.Cells["NgayHenTra"].Value);
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("Lỗi khi chuyển đổi dữ liệu. Vui lòng kiểm tra dữ liệu của hàng được chọn.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn hàng: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string theDG = txtTheDG.Text.Trim();
                string maNV = txtMaNV.Text.Trim();
                string maPM = txt_maPM.Text.Trim();

                DataTable dt = connection.TimKiemPhieuMuon(theDG, maNV, maPM);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phiếu mượn nào.");
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL khi tìm kiếm: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm phiếu mượn: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HienThiThongTinMuonSach();
            resetThongTin();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int kiemtraid;

            if (int.TryParse(textBox1.Text, out kiemtraid))
            {
                if (connection.tontaiTheDocGia(kiemtraid))
                {
                    DataTable readerInfo = connection.LayThongTinBanDoc(kiemtraid);

                    dataGridView2.DataSource = readerInfo;

                    if (readerInfo.Rows.Count == 0 || readerInfo.Rows[0]["TenSach"].ToString() == "Không tìm thấy")
                    {
                        MessageBox.Show("Không tìm thấy thông tin mượn sách của độc giả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Số thẻ độc giả không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView2.DataSource = null; 
                }
            }
            else
            {
                dataGridView2.DataSource = null; 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
    }
}
