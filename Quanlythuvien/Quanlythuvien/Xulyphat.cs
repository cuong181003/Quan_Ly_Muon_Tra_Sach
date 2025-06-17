using System;
using System.Data;
using System.Windows.Forms;

namespace Quanlythuvien
{
    public partial class Xulyphat : Form
    {
        Connection_SQL connection = new Connection_SQL();
        string manvLogin;

        public Xulyphat(string manvLogin)
        {
            InitializeComponent();
            HienThi();
            this.manvLogin = manvLogin;
            txtMaNV.Text = manvLogin;
            txtMaNV.Enabled= false;
            textBox1.Enabled= false;
        }

        private void HienThi()
        {
            try
            {
                DataTable phat = connection.dsHoaDonPhat();
                dataGridView1.DataSource = phat;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn phạt: " + ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    txt_soHoaDon.Text = row.Cells["SoHoaDon"].Value.ToString();
                    txtTheDG.Text = row.Cells["SoTheDG"].Value.ToString();
                    txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                    txt_tienPhat.Text = row.Cells["Tienphat"].Value.ToString();
                    txt_lyDo.Text = row.Cells["LyDo"].Value.ToString();
                    dtp_ngayPhat.Value = DateTime.Parse(row.Cells["NgayPhat"].Value.ToString());
                    txt_hinhthuc.Text = row.Cells["HinhThuc"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_soHoaDon.Text) || string.IsNullOrEmpty(txtTheDG.Text) || string.IsNullOrEmpty(txt_lyDo.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!");
                    return;
                }


                connection.themHoaDonPhat(txt_soHoaDon.Text, txtTheDG.Text, manvLogin, textBox1.Text, txt_lyDo.Text, dtp_ngayPhat.Value, txt_hinhthuc.Text);
                MessageBox.Show("Thêm hóa đơn phạt thành công!");
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hóa đơn phạt: " + ex.Message);
            }
        }


        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_soHoaDon.Text) ||
                    string.IsNullOrEmpty(txtTheDG.Text) ||
                    string.IsNullOrEmpty(txtMaNV.Text) ||
                    string.IsNullOrEmpty(txt_lyDo.Text) ||
                    string.IsNullOrEmpty(txt_hinhthuc.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!");
                    return;
                }

                string soHoaDon = txt_soHoaDon.Text;
                string soTheDG = txtTheDG.Text;
                string maNV = manvLogin;
                string lydo = txt_lyDo.Text;
                DateTime ngayphat = dtp_ngayPhat.Value;
                string hinhthuc = txt_hinhthuc.Text;

                connection.suaHoaDonPhat(soHoaDon, soTheDG, maNV, textBox1.Text, lydo, ngayphat, hinhthuc);
                MessageBox.Show("Sửa hóa đơn phạt thành công!");
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa hóa đơn phạt: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_soHoaDon.Text))
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn phạt để xóa!");
                    return;
                }

                string soHD = txt_soHoaDon.Text;

                bool iskiemtra = connection.tontaihoadon(soHD);
                if (!iskiemtra)
                {
                    MessageBox.Show("Hóa đơn phạt không tồn tại trong hệ thống.");
                    return;
                }

                connection.xoaHoaDonPhat(soHD);
                MessageBox.Show("Xóa hóa đơn phạt thành công!");
                HienThi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn phạt: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string theDG = txtTheDG.Text.Trim();
                string maNV = txtMaNV.Text.Trim();
                string soHoaDon = txt_soHoaDon.Text.Trim();
                DataTable dt = connection.timKiemHoaDonPhat(soHoaDon, theDG, maNV);

                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn phạt nào với thông tin bạn đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm hóa đơn phạt: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HienThi();
            txt_soHoaDon.Text = "";
            txtTheDG.Text = "";
            txtMaNV.Text = manvLogin;
            txt_tienPhat.Text = "";
            txt_lyDo.Text = "";
            txt_hinhthuc.Text = "";
            comboBox1.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

                try
                {
                    decimal tienSach = decimal.Parse( txt_tienPhat.Text), tienPhatCoBan = 50000;
                 

                    string selectedValue = comboBox1.SelectedItem?.ToString();
                    if (!string.IsNullOrEmpty(selectedValue) && selectedValue.EndsWith("%"))
                    {
                        int phanTram = int.Parse(selectedValue.TrimEnd('%'));
                        decimal tienPhatHuHai = tienSach * phanTram / 100;

                        decimal tongTienPhat = tienPhatHuHai + tienPhatCoBan;

                      //  txt_tienPhat.Text = $"{tienPhatCoBan}" ; 
                        textBox1.Text = $"{tongTienPhat}" ;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn tình trạng hư hại hợp lệ!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tính toán tổng tiền phạt: " + ex.Message);
                }
            }


        
        
    }
}
