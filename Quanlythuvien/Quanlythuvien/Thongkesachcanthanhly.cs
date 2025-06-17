using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Quanlythuvien
{
    public partial class Thongkesachcanthanhly : Form
    {           
        Connection_SQL connection = new Connection_SQL();
        string maNV;
        public Thongkesachcanthanhly(string maNV)
        {
            InitializeComponent(); HienThiThongKe();
            this.maNV = maNV;
        }

        private void HienThiThongKe()
        {
            DataTable dtThongKe = connection.LaySachCanThanhLy();
            dataGridView1.DataSource = dtThongKe;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HienThiThongKe();
            txtMaSach.Text = "";
            txtTenSachThatLac.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string maSach = txtMaSach.Text.Trim();
                string tenSach = txtTenSachThatLac.Text.Trim();

                Connection_SQL connection = new Connection_SQL();
                DataTable result = connection.LaySachCanThanhLy(maSach, tenSach);

                if (result.Rows.Count > 0)
                {
                    dataGridView1.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sách cần thanh lý với các thông tin đã nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sách cần thanh lý: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string laycotExcel(int laycot)
        {
            int n = laycot;
            string tencot = String.Empty;
            int i;

            while (n > 0)
            {
                i = (n - 1) % 26;
                tencot = Convert.ToChar(65 + i) + tencot;
                n = (n - i) / 26;
            }

            return tencot;
        }

        private void ExportToExcel(DataGridView dataGridView, string luu, string tenNV)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Name = "Data";

                string libraryName = "Thư viện UNETI";
                worksheet.Cells[1, 1] = libraryName;
                Excel.Range libraryNameRange = worksheet.get_Range("A1", laycotExcel(dataGridView.Columns.Count) + "1");
                libraryNameRange.Merge();
                libraryNameRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                libraryNameRange.Font.Size = 16;
                libraryNameRange.Font.Bold = true;

                string TitleBaocao = "Thống kê sách cần thanh lý";
                worksheet.Cells[2, 1] = TitleBaocao;
                Excel.Range titleRange1 = worksheet.get_Range("A2", laycotExcel(dataGridView.Columns.Count) + "2");
                titleRange1.Merge();
                titleRange1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                titleRange1.Font.Size = 14;
                titleRange1.Font.Bold = true;

                for (int i = 1; i < dataGridView.Columns.Count + 1; i++)
                {
                    worksheet.Cells[3, i] = dataGridView.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 4, j + 1] = dataGridView.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }

                string date = DateTime.Now.ToString("dd/MM/yyyy");
                Excel.Range dateCell = worksheet.Cells[dataGridView.Rows.Count + 6, dataGridView.Columns.Count];
                dateCell.Value = "Ngày xuất: " + date;
                dateCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                Excel.Range employeeCell = worksheet.Cells[dataGridView.Rows.Count + 7, dataGridView.Columns.Count];
                employeeCell.Value = "Nhân viên: " + tenNV;
                employeeCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                Excel.Range usedRange = worksheet.UsedRange;
                usedRange.Columns.AutoFit();

                workbook.SaveAs(luu);
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show("Dữ liệu đã được xuất và lưu thành công tại " + luu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }
        private void btn_boqua_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Lưu tệp Excel";
            saveFileDialog.FileName = "ThongKeSachCanThanhLy.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string tenNV = connection.LayTenNV(maNV);

                ExportToExcel(dataGridView1, saveFileDialog.FileName, tenNV);
            }

            
        }
    }
}
