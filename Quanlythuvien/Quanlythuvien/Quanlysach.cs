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
    public partial class Quanlysach : Form
    {
        Connection_SQL connection = new Connection_SQL();

        public Quanlysach()
        {
            InitializeComponent(); HienThiSach();
        }
        private void HienThiSach()
        {
            DataTable dtSach = connection.LaySach();  
            dataGridView1.DataSource = dtSach;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            HienThiSach();
            txt_maSach.Text = "";
            txt_nxb.Text = "";
            txt_tacGia.Text = "";
            txt_tenSach.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string maSach = txt_maSach.Text.Trim();
                string tenSach = txt_tenSach.Text.Trim();
                string tacGia = txt_tacGia.Text.Trim();
                string nxb = txt_nxb.Text.Trim();

                if (string.IsNullOrEmpty(maSach) && string.IsNullOrEmpty(tenSach) && string.IsNullOrEmpty(tacGia) && string.IsNullOrEmpty(nxb))
                {
                    MessageBox.Show("Vui lòng nhập ít nhất 1 mã sách, tên sách hoặc nhà xuất bản để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable result = connection.TimKiemSach(maSach, tenSach, tacGia, nxb);

                if (result.Rows.Count > 0)
                {
                    dataGridView1.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sách nào phù hợp với tiêu chí tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
