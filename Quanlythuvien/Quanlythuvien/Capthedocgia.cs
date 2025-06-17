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
    public partial class Capthedocgia : Form
    {
            Connection_SQL connection = new Connection_SQL();

        public Capthedocgia()
        {
            InitializeComponent();
            HienThiTheDocGia();
        }
        private void HienThiTheDocGia()
        {
            DataTable dtTheDocGia = connection.LayTheDocGia();
            dataGridView1.DataSource = dtTheDocGia;
        }
        bool KtRong()
        {
            if (txt_sotheDG.Text == "") return false;
            if (txtMaDg.Text == "") return false;
            if (dtp_ngaycap.Text == "") return false;
            if (dtp_handung.Text == "") return false;
            return true;
        }
        void resetThongTin()
        {
            txt_sotheDG.Text = "";
            txtMaDg.Text = "";
            dtp_ngaycap.Text = "";
            dtp_handung.Text = "";
            txt_sotheDG.Focus();
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
                int soTheDG = int.Parse(txt_sotheDG.Text);
                if (connection.tontaiTheDocGia(soTheDG))
                {
                    MessageBox.Show("Số thẻ độc giả đã tồn tại!", "Thông báo");
                    return;
                }

                int maDG = int.Parse(txtMaDg.Text);

                if (connection.DocGiaDaCapThe(maDG))
                {
                    MessageBox.Show("Độc giả này đã được cấp thẻ trước đó!", "Thông báo");
                    return;
                }
                if (!connection.tontaiDocGia(maDG))
                {
                    MessageBox.Show("Mã độc giả không tồn tại!", "Thông báo");
                    return;
                }

                DateTime ngayCap = dtp_ngaycap.Value;
                DateTime hanDung = dtp_handung.Value;
                connection.ThemTheDocGia(soTheDG, maDG, ngayCap, hanDung);
                MessageBox.Show("Thêm thẻ độc giả thành công!");
                HienThiTheDocGia();
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi: Vui lòng nhập đúng định dạng số cho mã thẻ và mã độc giả!", "Thông báo");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo");
            }
        }
        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                int soTheDG = int.Parse(txt_sotheDG.Text);
                int maDG = int.Parse(txtMaDg.Text);

                if (!connection.tontaiDocGia(maDG))
                {
                    MessageBox.Show("Mã độc giả không tồn tại!", "Thông báo");
                    return;
                }


                DateTime ngayCap = dtp_ngaycap.Value;
                DateTime hanDung = dtp_handung.Value;

                connection.SuaTheDocGia(soTheDG, maDG, ngayCap, hanDung);
                MessageBox.Show("Sửa thẻ độc giả thành công!");
                HienThiTheDocGia(); 
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi: Vui lòng nhập đúng định dạng số cho mã thẻ và mã độc giả!", "Thông báo");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    txt_sotheDG.Text = row.Cells["SoTheDG"].Value.ToString();
                    txtMaDg.Text = row.Cells["MaDG"].Value.ToString();
                    dtp_ngaycap.Value = Convert.ToDateTime(row.Cells["NgayCap"].Value);
                    dtp_handung.Value = Convert.ToDateTime(row.Cells["HanDung"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sotheDG.Text))
            {
                int soTheDG = int.Parse(txt_sotheDG.Text);

                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa thẻ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connection.XoaTheDocGia(soTheDG);  
                        MessageBox.Show("Xóa thẻ độc giả thành công!");
                        HienThiTheDocGia();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thẻ độc giả để xóa.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string soTheDG = txt_sotheDG.Text.Trim();
            string maDG = txtMaDg.Text.Trim();

            DataTable dt = connection.TimKiemTheDocGia(soTheDG, maDG);

            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thẻ độc giả nào.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HienThiTheDocGia();
            resetThongTin();
        }

        private void btn_inThe_Click(object sender, EventArgs e)
        {
            if (txt_sotheDG.Text == "")
            {
                MessageBox.Show("Chọn 1 số thẻ độc giả cần in!", "Thông báo");
                return;
            }
            else
            {
                if (!connection.tontaiTheDocGia(int.Parse(txt_sotheDG.Text)))
                {
                    MessageBox.Show("Số thẻ độc giả không tồn tại!", "Thông báo");
                    return;
                }

                The formThe = new The(int.Parse(txt_sotheDG.Text));
                formThe.Show();
             //   formThe.Hide();
              //  formThe.PrintForm();
            }
        }

        private void btngiahan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sotheDG.Text))
            {
                try
                {
                    int soTheDG = int.Parse(txt_sotheDG.Text);

                    if (!connection.tontaiTheDocGia(soTheDG))
                    {
                        MessageBox.Show("Số thẻ độc giả không tồn tại!", "Thông báo");
                        return;
                    }

                    DateTime kiemtra = dtp_handung.Value;

                    if (kiemtra <= DateTime.Now)
                    {
                        DateTime hanmoi = DateTime.Now.AddYears(1);

                        connection.SuaHanDungTheDocGia(soTheDG, hanmoi);

                        MessageBox.Show("Gia hạn thẻ độc giả thành công! Thẻ mới có hiệu lực đến " + hanmoi.ToString("dd/MM/yyyy"), "Thông báo");

                        HienThiTheDocGia();
                    }
                    else
                    {
                        MessageBox.Show("Thẻ độc giả vẫn còn hiệu lực. Không cần gia hạn.", "Thông báo");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lỗi: Vui lòng nhập đúng định dạng số cho số thẻ độc giả!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thẻ độc giả để gia hạn.", "Thông báo");
            }
        }
    }
}
