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
    public partial class Quanlytheloaisach : Form
    {
        Connection_SQL connection = new Connection_SQL();

        public Quanlytheloaisach()
        {
            InitializeComponent();
            HienThiLoaiSach();
        }
        private void HienThiLoaiSach()
        {
            DataTable dtLoaiSach = connection.LayLoaiSach(); 
            dataGridView1.DataSource = dtLoaiSach;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string maLoaiSach = txt_maLoaiSach.Text.Trim();
                string tenLoaiSach = txt_tenLoaiSach.Text.Trim();

                if (string.IsNullOrEmpty(maLoaiSach) && string.IsNullOrEmpty(tenLoaiSach))
                {
                    MessageBox.Show("Vui lòng nhập ít nhất một mã sáh hoặc tên sách để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Connection_SQL connection = new Connection_SQL();
                DataTable result = connection.TimKiemLoaiSach(maLoaiSach, tenLoaiSach);

                if (result.Rows.Count > 0)
                {
                    dataGridView1.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại sách nào phù hợp với tiêu chí tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button2_Click(object sender, EventArgs e)
        {
            HienThiLoaiSach();
            txt_maLoaiSach.Text = "";
            txt_tenLoaiSach.Text = "";
        }
    }
}
