using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Quanlythuvien
{
    public partial class Thongtindocgia : Form
    {
        Connection_SQL connection = new Connection_SQL();

        public Thongtindocgia()
        {
            InitializeComponent();
            HienThiDocGia();
            button3.Visible = false;
        }

        private void HienThiDocGia()
        {
                DataTable dtDocGia = connection.LayDocGia();
                dataGridView1.DataSource = dtDocGia;
        }

   
        bool KtRong()
        {
            if (txt_maDG.Text == "") return false;
            if (txt_hotenDG.Text == "") return false;
            if (txt_sdtDG.Text == "") return false;
            if (txt_emailDG.Text == "") return false;
            if (txt_diachiDG.Text == "") return false; if (txt_cccd.Text == "") return false;

            return true;
        }

        void resetThongTin()
        {
            txt_maDG.Text = "";
            txt_hotenDG.Text = "";
            rdo_Nam.Checked = true;
            dtp_ngaysinhDG.Text = "";
            txt_sdtDG.Text = "";
            txt_emailDG.Text = "";
            txt_cccd.Text = "";
            txt_diachiDG.Text = "";
            txt_maDG.Focus();
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

                string cccd = txt_cccd.Text.Trim();
                if (connection.KiemTraCCCDTonTai(cccd))
                {
                    MessageBox.Show("CCCD đã tồn tại!", "Thông báo");
                    return;
                }

                int maDG = int.Parse(txt_maDG.Text.Trim());
                if (connection.tontaiDocGia(maDG))
                {
                    MessageBox.Show("Mã độc giả đã tồn tại!", "Thông báo");
                    return;
                }

                string hoTenDG = txt_hotenDG.Text.Trim();
                string gioiTinh = rdo_Nam.Checked ? "Nam" : "Nữ";
                DateTime ngaySinh = dtp_ngaysinhDG.Value;
                string sdt = txt_sdtDG.Text.Trim();
                string email = txt_emailDG.Text.Trim();
                string diaChi = txt_diachiDG.Text.Trim();

                connection.ThemDocGia(maDG, hoTenDG, gioiTinh, ngaySinh, sdt, email, diaChi, cccd);
                MessageBox.Show("Thêm độc giả thành công!");
                HienThiDocGia();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm độc giả: " + ex.Message);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KtRong())
                {
                    MessageBox.Show("Chưa nhập đủ thông tin!", "Thông báo");
                    return;
                }

                int maDG = int.Parse(txt_maDG.Text.Trim());
                string cccd = txt_cccd.Text.Trim();
                if (connection.KiemTraCCCDTonTai(cccd, maDG))
                {
                    MessageBox.Show("CCCD đã tồn tại ở độc giả khác!", "Thông báo");
                    return;
                }

                string hoTenDG = txt_hotenDG.Text.Trim();
                string gioiTinh = rdo_Nam.Checked ? "Nam" : "Nữ";
                DateTime ngaySinh = dtp_ngaysinhDG.Value;
                string sdt = txt_sdtDG.Text.Trim();
                string email = txt_emailDG.Text.Trim();
                string diaChi = txt_diachiDG.Text.Trim();

                connection.SuaDocGia(maDG, hoTenDG, gioiTinh, ngaySinh, sdt, email, diaChi, cccd);
                MessageBox.Show("Sửa độc giả thành công!");
                HienThiDocGia();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa độc giả: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int maDG = int.Parse(txt_maDG.Text.Trim());
                    connection.XoaDocGia(maDG);
                    MessageBox.Show("Xóa độc giả thành công!");
                    HienThiDocGia();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa độc giả: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string maDG = txt_maDG.Text.Trim();
                string hoTenDG = txt_hotenDG.Text.Trim();
                string email = txt_emailDG.Text.Trim();
                string sdt = txt_sdtDG.Text.Trim();
                string cccd = txt_cccd.Text.Trim();

                DataTable dt = connection.TimKiemDocGia(maDG, hoTenDG, email, sdt , cccd);

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy độc giả nào.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm độc giả: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    txt_maDG.Text = row.Cells["MaDG"].Value.ToString();
                    txt_hotenDG.Text = row.Cells["HoTenDG"].Value.ToString();
                    string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                    rdo_Nam.Checked = (gioiTinh == "Nam");
                    rdo_Nu.Checked = (gioiTinh == "Nữ");
                    dtp_ngaysinhDG.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    txt_sdtDG.Text = row.Cells["SDT"].Value.ToString();
                    txt_emailDG.Text = row.Cells["Email"].Value.ToString();
                    txt_diachiDG.Text = row.Cells["DiaChi"].Value.ToString();
                    txt_cccd.Text = row.Cells["CCCD"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn hàng: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int maDG = int.Parse(txt_maDG.Text.Trim());

                DataTable dtKiemTra = connection.KiemTraMuonQuaHan(maDG);

                if (dtKiemTra.Rows.Count > 0)
                {
                    StringBuilder result = new StringBuilder("Tình trạng mượn sách của độc giả:\n");
                    bool isSachChuaTra = false;

                    foreach (DataRow row in dtKiemTra.Rows)
                    {
                        string tenSach = row["TenSach"].ToString();
                        DateTime ngayMuon = Convert.ToDateTime(row["NgayMuon"]);
                        DateTime ngayHenTra = Convert.ToDateTime(row["NgayHenTra"]);
                        string trangThai = row["TrangThai"].ToString();

                        if (trangThai == "Chưa trả")
                        {
                            isSachChuaTra = true;
                            int timeQuaHan = (ngayHenTra - DateTime.Now).Days;

                            if (timeQuaHan < 0)
                            {
                                result.AppendLine($"- Tên sách: {tenSach}, Ngày mượn: {ngayMuon.ToShortDateString()}, Ngày hẹn trả: {ngayHenTra.ToShortDateString()}, Tình trạng: Quá hạn {Math.Abs(timeQuaHan)} ngày.");
                            }
                            else
                            {
                                result.AppendLine($"- Tên sách: {tenSach}, Ngày mượn: {ngayMuon.ToShortDateString()}, Ngày hẹn trả: {ngayHenTra.ToShortDateString()}, Tình trạng: Còn {timeQuaHan} ngày đến hạn.");
                            }
                        }
                    }

                    if (isSachChuaTra)
                    {
                        MessageBox.Show(result.ToString(), "Kết quả kiểm tra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Độc giả này không có sách chưa trả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Độc giả này không có sách mượn hoặc chưa trả quá hạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra tình trạng mượn sách: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HienThiDocGia();
            resetThongTin();
        }
    }
}
