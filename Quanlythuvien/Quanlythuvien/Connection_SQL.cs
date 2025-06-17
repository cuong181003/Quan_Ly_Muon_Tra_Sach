using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Quanlythuvien
{
    internal class Connection_SQL
    {
      //  string strCon = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Application.StartupPath}\QuanLyMuonTra.mdf;Integrated Security=True;Connect Timeout=30";
        //string strCon = $@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=test;Integrated Security=True";

        SqlConnection sqlCon = null;
        private string strCon;

        public Connection_SQL()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string targetFolder = Path.Combine(appDataPath, "QuanLyMuonTra");
                string targetFile = Path.Combine(targetFolder, "QuanLyMuonTra.mdf");

                // Đường dẫn nguồn từ Program Files
                string sourceFile = Path.Combine(Application.StartupPath, "QuanLyMuonTra.mdf");

                // Kiểm tra và tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }

                // Sao chép tệp nếu chưa tồn tại trong AppData
                if (!File.Exists(targetFile))
                {
                    if (File.Exists(sourceFile))
                    {
                        File.Copy(sourceFile, targetFile, true);
                        Console.WriteLine("Cơ sở dữ liệu đã được sao chép vào AppData.");
                    }
                    else
                    {
                        Console.WriteLine("Tệp nguồn không tồn tại: " + sourceFile);
                    }
                }
                else
                {
                    Console.WriteLine("Cơ sở dữ liệu đã tồn tại trong AppData.");
                }

                // Chuỗi kết nối
                strCon = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={targetFile};Integrated Security=True;Connect Timeout=30";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo kết nối: " + ex.Message);
            }
        }



        public void MoKetNoi()
        {
            try
            {
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);
                }

                if (sqlCon.State == System.Data.ConnectionState.Closed)
                {
                    sqlCon.Open();
                    Console.WriteLine("Kết nối thành công!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi kết nối: " + ex.Message);
            }
        }

        public void DongKetNoi()
        {
            if (sqlCon != null && sqlCon.State == System.Data.ConnectionState.Open)
            {
                sqlCon.Close();
                Console.WriteLine("Đóng kết nối thành công!");
            }
            else
            {
                Console.WriteLine("Chưa tạo kết nối");
            }
        }

        // login
        public bool KiemTraDangNhap(string maNV, string matKhau)
        {
            try
            {
                MoKetNoi();  
                string query = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaNV", maNV);  
                cmd.Parameters.AddWithValue("@MatKhau", matKhau); 
                int result = (int)cmd.ExecuteScalar();  
                DongKetNoi(); 
                return result > 0; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra đăng nhập: " + ex.Message);
                return false;
            }
        }
        public string LayTenNV(string maNV)
        {

                MoKetNoi();
                string query = "SELECT HoTenNV FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                object result = cmd.ExecuteScalar();
                DongKetNoi();

                if (result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return string.Empty;
                }
            

        }

        // form thông tin cá nhân
        public DataTable LayThongTinCaNhan(string maNV)
        {
            MoKetNoi();
            string query = "SELECT HoTenNV, GioiTinh, NgaySinh, SDT, Email FROM NhanVien WHERE MaNV = @MaNV";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaNV", maNV);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DongKetNoi();
            return dt;
        }

        public bool SuaNV(string maNV, string hoTen, DateTime ngaySinh, string gioiTinh, string sdt, string email)
        {
            try
            {
                MoKetNoi();
                string query = "UPDATE NhanVien SET HoTenNV = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, SDT = @SDT, Email = @Email WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@Email", email);

                int rowsAffected = cmd.ExecuteNonQuery();
                DongKetNoi();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin: " + ex.Message);
                return false;
            }
        }

        public bool KiemTraMatKhauCu(string maNV, string matKhauCu)
        {
            MoKetNoi();
            string query = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV AND MatKhau = @MatKhau";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaNV", maNV);
            cmd.Parameters.AddWithValue("@MatKhau", matKhauCu);
            int result = (int)cmd.ExecuteScalar();
            DongKetNoi();
            return result > 0;
        }

        public bool CapNhatMatKhau(string maNV, string matKhauMoi)
        {
            MoKetNoi();
            string query = "UPDATE NhanVien SET MatKhau = @MatKhauMoi WHERE MaNV = @MaNV";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
            cmd.Parameters.AddWithValue("@MaNV", maNV);
            int result = cmd.ExecuteNonQuery();
            DongKetNoi();
            return result > 0;
        }


        // form quản lý sách
        public DataTable LaySach()
        {
            MoKetNoi();
            string query = "SELECT MaSach, TenSach, NXB, TacGia, MaLoaiSach, ViTri, SoLuong, SoLuongMuon, TinhTrang FROM Sach";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlCon);
            DataTable dtSach = new DataTable();
            adapter.Fill(dtSach);
            DongKetNoi();  return dtSach;
          
        }
        public DataTable TimKiemSach(string maSach, string tenSach, string tacGia, string nxb)
        {
            MoKetNoi();
            string query = "SELECT MaSach, TenSach, NXB, TacGia, MaLoaiSach, ViTri, SoLuong, SoLuongMuon, TinhTrang FROM Sach WHERE 1=1";

            if (!string.IsNullOrEmpty(maSach))
            {
                query += " AND MaSach LIKE @MaSach";
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                query += " AND TenSach LIKE @TenSach";
            }
            if (!string.IsNullOrEmpty(tacGia))
            {
                query += " AND TacGia LIKE @TacGia";
            }
            if (!string.IsNullOrEmpty(nxb))
            {
                query += " AND NXB LIKE @NXB";
            }

            SqlCommand cmd = new SqlCommand(query, sqlCon);

            if (!string.IsNullOrEmpty(maSach))
            {
                cmd.Parameters.AddWithValue("@MaSach", "%" + maSach + "%");
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                cmd.Parameters.AddWithValue("@TenSach", "%" + tenSach + "%");
            }
            if (!string.IsNullOrEmpty(tacGia))
            {
                cmd.Parameters.AddWithValue("@TacGia", "%" + tacGia + "%");
            }
            if (!string.IsNullOrEmpty(nxb))
            {
                cmd.Parameters.AddWithValue("@NXB", "%" + nxb + "%");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtKetQuaTimKiem = new DataTable();
            adapter.Fill(dtKetQuaTimKiem);
            DongKetNoi();

            return dtKetQuaTimKiem;
        }


        // form quản lý sách
        public DataTable LayLoaiSach()
        {
            MoKetNoi();
            string query = "SELECT MaLoaiSach, TenLoaiSach FROM LoaiSach";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlCon);
            DataTable dtLoaiSach = new DataTable();
            adapter.Fill(dtLoaiSach);
           DongKetNoi();  return dtLoaiSach;
           
        }
        public DataTable TimKiemLoaiSach(string maLoaiSach, string tenLoaiSach)
        {
            MoKetNoi();

            string query = "SELECT MaLoaiSach, TenLoaiSach FROM LoaiSach WHERE 1=1";

            if (!string.IsNullOrEmpty(maLoaiSach))
            {
                query += " AND MaLoaiSach LIKE @MaLoaiSach";
            }
            if (!string.IsNullOrEmpty(tenLoaiSach))
            {
                query += " AND TenLoaiSach LIKE @TenLoaiSach";
            }

            SqlCommand cmd = new SqlCommand(query, sqlCon);

            if (!string.IsNullOrEmpty(maLoaiSach))
            {
                cmd.Parameters.AddWithValue("@MaLoaiSach", "%" + maLoaiSach + "%");
            }
            if (!string.IsNullOrEmpty(tenLoaiSach))
            {
                cmd.Parameters.AddWithValue("@TenLoaiSach", "%" + tenLoaiSach + "%");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtKetQuaTimKiem = new DataTable();
            adapter.Fill(dtKetQuaTimKiem);
            DongKetNoi();

            return dtKetQuaTimKiem;
        }


        // form quản lý độc giả
        public DataTable LayDocGia()
        {
            MoKetNoi();
            // string query = "SELECT MaDG, HoTenDG, GioiTinh, NgaySinh, SDT, Email, DiaChi FROM DocGia";
            string query = "SELECT MaDG,CCCD, HoTenDG, GioiTinh, NgaySinh, SDT, Email, DiaChi FROM DocGia";

            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlCon);
            DataTable dtDocGia = new DataTable();
            adapter.Fill(dtDocGia);  
            DongKetNoi();
            return dtDocGia;
           
        }
        public void ThemDocGia(int maDG, string hoTenDG, string gioiTinh, DateTime ngaySinh, string sdt, string email, string diaChi, string cccd)
        {
            try
            {
                MoKetNoi();
                string query = "INSERT INTO DocGia (MaDG, HoTenDG, GioiTinh, NgaySinh, SDT, Email, DiaChi, CCCD) " +
                               "VALUES (@MaDG, @HoTenDG, @GioiTinh, @NgaySinh, @SDT, @Email, @DiaChi, @CCCD)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaDG", maDG);
                cmd.Parameters.AddWithValue("@HoTenDG", hoTenDG);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@CCCD", cccd);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }

        public void SuaDocGia(int maDG, string hoTenDG, string gioiTinh, DateTime ngaySinh, string sdt, string email, string diaChi, string cccd)
        {
            try
            {
                MoKetNoi();
                string query = "UPDATE DocGia SET HoTenDG = @HoTenDG, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, " +
                               "SDT = @SDT, Email = @Email, DiaChi = @DiaChi, CCCD = @CCCD WHERE MaDG = @MaDG";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaDG", maDG);
                cmd.Parameters.AddWithValue("@HoTenDG", hoTenDG);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@CCCD", cccd);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }

        public void XoaDocGia(int maDG)
        {
            try
            {
                MoKetNoi();
                string query = "DELETE FROM DocGia WHERE MaDG = @MaDG";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaDG", maDG);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }
        public DataTable TimKiemDocGia(string maDG, string hoTenDG, string email, string sdt,string cccd)
        {
            MoKetNoi();
            string query = "SELECT MaDG, HoTenDG, GioiTinh, NgaySinh, SDT, Email, DiaChi FROM DocGia WHERE 1=1";

            if (!string.IsNullOrEmpty(maDG))
            {
                query += " AND MaDG LIKE @MaDG";
            }
            if (!string.IsNullOrEmpty(hoTenDG))
            {
                query += " AND HoTenDG LIKE @HoTenDG";
            }
            if (!string.IsNullOrEmpty(email))
            {
                query += " AND Email LIKE @Email";
            }
            if (!string.IsNullOrEmpty(sdt))
            {
                query += " AND SDT LIKE @SDT";
            }
            if (!string.IsNullOrEmpty(cccd))
            {
                query += " AND CCCD LIKE @CCCD";
                
            }
            SqlCommand cmd = new SqlCommand(query, sqlCon);

            if (!string.IsNullOrEmpty(maDG))
            {
                cmd.Parameters.AddWithValue("@MaDG", "%" + maDG + "%");
            }
            if (!string.IsNullOrEmpty(hoTenDG))
            {
                cmd.Parameters.AddWithValue("@HoTenDG", "%" + hoTenDG + "%");
            }
            if (!string.IsNullOrEmpty(email))
            {
                cmd.Parameters.AddWithValue("@Email", "%" + email + "%");
            }
            if (!string.IsNullOrEmpty(sdt))
            {
                cmd.Parameters.AddWithValue("@SDT", "%" + sdt + "%");
            }
            if (!string.IsNullOrEmpty(cccd))
            {
                cmd.Parameters.AddWithValue("@CCCD", "%" + cccd + "%");
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtKetQuaTimKiem = new DataTable();
            adapter.Fill(dtKetQuaTimKiem);
            DongKetNoi();

            return dtKetQuaTimKiem;
        }
        public DataTable KiemTraMuonQuaHan(int maDG)
        {
            MoKetNoi();
            string query = @"
        SELECT pm.MaPM, s.TenSach, pm.NgayMuon, pm.NgayHenTra, 
               CASE WHEN pm.TrangThai = 1 THEN N'Đã trả' ELSE N'Chưa trả' END AS TrangThai,
               CASE WHEN pm.NgayHenTra < GETDATE() AND pm.TrangThai = 0 THEN N'Quá hạn' ELSE N'Đúng hạn' END AS TinhTrang
        FROM PhieuMuon pm
        INNER JOIN ChiTietPM ctp ON pm.MaPM = ctp.MaPM
        INNER JOIN Sach s ON ctp.MaSach = s.MaSach
        INNER JOIN TheDocGia tdg ON pm.SoTheDG = tdg.SoTheDG
        WHERE tdg.MaDG = @MaDG";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaDG", maDG);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtKiemTra = new DataTable();
            adapter.Fill(dtKiemTra);
            DongKetNoi();

            return dtKiemTra;
        }
        public bool tontaiDocGia(int madg)
        {
            MoKetNoi();
            string query = "SELECT * FROM dbo.DocGia where MaDG = @ma";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("ma", madg);
            SqlDataReader dr = cmd.ExecuteReader();
            bool kt = false;
            if (dr.HasRows) kt = true;
            DongKetNoi();
            return kt;
        }

        public bool KiemTraCCCDTonTai(string cccd, int? maDG = null)
        {
            MoKetNoi() ;
                string query = "SELECT COUNT(*) FROM DocGia WHERE CCCD = @CCCD";
                if (maDG.HasValue)
                {
                    query += " AND MaDG != @MaDG";
                }

            SqlCommand cmd = new SqlCommand(query, sqlCon);

            cmd.Parameters.AddWithValue("@CCCD", cccd);
                    if (maDG.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@MaDG", maDG.Value);
                    }
                    int count = (int)cmd.ExecuteScalar();
                                  DongKetNoi();
  return count > 0;
            
        }


        // form quản lý thẻ độc giả
        public bool tontaiTheDocGia(int sotheDG)
        {
            MoKetNoi();
            string query = "SELECT * FROM dbo.TheDocGia WHERE SoTheDG = @sotheDG";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("sotheDG", sotheDG);
            SqlDataReader dr = cmd.ExecuteReader();
            bool kt = false;
            if (dr.HasRows) kt = true;
            DongKetNoi();
            return kt;
        }
        public bool DocGiaDaCapThe(int maDG)
        {
            try
            {
                MoKetNoi();
                string query = "SELECT COUNT(*) FROM TheDocGia WHERE MaDG = @MaDG";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaDG", maDG);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch
            {
                return true; 
            }
            finally
            {
                DongKetNoi();
            }
        }
        public void ThemTheDocGia(int soTheDG, int maDG, DateTime ngayCap, DateTime hanDung)
        {
            try
            {
                MoKetNoi();
                string query = "INSERT INTO TheDocGia (SoTheDG, MaDG, NgayCap, HanDung) " +
                               "VALUES (@SoTheDG, @MaDG, @NgayCap, @HanDung)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
                cmd.Parameters.AddWithValue("@MaDG", maDG);
                cmd.Parameters.AddWithValue("@NgayCap", ngayCap);
                cmd.Parameters.AddWithValue("@HanDung", hanDung);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }
        public void SuaTheDocGia(int soTheDG, int maDG, DateTime ngayCap, DateTime hanDung)
        {
            try
            {
                MoKetNoi();
                string query = "UPDATE TheDocGia SET MaDG = @MaDG, NgayCap = @NgayCap, HanDung = @HanDung " +
                               "WHERE SoTheDG = @SoTheDG";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
                cmd.Parameters.AddWithValue("@MaDG", maDG);
                cmd.Parameters.AddWithValue("@NgayCap", ngayCap);
                cmd.Parameters.AddWithValue("@HanDung", hanDung);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }
        public void XoaTheDocGia(int soTheDG)
        {
            try
            {
                MoKetNoi();
                string query = "DELETE FROM TheDocGia WHERE SoTheDG = @SoTheDG";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }
        public DataTable LayTheDocGia()
        {
            MoKetNoi();
            string query = "SELECT SoTheDG, MaDG, NgayCap, HanDung FROM TheDocGia";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlCon);
            DataTable dtTheDocGia = new DataTable();
            adapter.Fill(dtTheDocGia);
          DongKetNoi();   return dtTheDocGia;
           
        }
        public DataTable TimKiemTheDocGia(string soTheDG, string maDG)
        {
            MoKetNoi();
            string query = "SELECT SoTheDG, MaDG, NgayCap, HanDung FROM TheDocGia WHERE 1=1";

            if (!string.IsNullOrEmpty(soTheDG))
            {
                query += " AND SoTheDG LIKE @SoTheDG";
            }
            if (!string.IsNullOrEmpty(maDG))
            {
                query += " AND MaDG LIKE @MaDG";
            }

            SqlCommand cmd = new SqlCommand(query, sqlCon);

            if (!string.IsNullOrEmpty(soTheDG))
            {
                cmd.Parameters.AddWithValue("@SoTheDG", "%" + soTheDG + "%");
            }
            if (!string.IsNullOrEmpty(maDG))
            {
                cmd.Parameters.AddWithValue("@MaDG", "%" + maDG + "%");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtKetQuaTimKiem = new DataTable();
            adapter.Fill(dtKetQuaTimKiem);
            DongKetNoi();

            return dtKetQuaTimKiem;
        }
        public void SuaHanDungTheDocGia(int soTheDG, DateTime hanDung)
        {
            try
            {
                MoKetNoi();
                string query = "UPDATE TheDocGia SET HanDung = @HanDung WHERE SoTheDG = @SoTheDG";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
                cmd.Parameters.AddWithValue("@HanDung", hanDung);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }


        // form mượn sách
        public DataTable LayThongTinMuonSach()
        {
            MoKetNoi();

            string query = @"SELECT MaPM, SoTheDG, MaNV , NgayMuon, NgayHenTra FROM PhieuMuon";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtThongTinMuonSach = new DataTable();
            adapter.Fill(dtThongTinMuonSach);
            DongKetNoi();

            return dtThongTinMuonSach;
        }
        public void ThemPhieuMuon(int maPM, int soTheDG, string maNV, DateTime ngayMuon, DateTime ngayHenTra)
        {
            try
            {
                MoKetNoi();
                string query = "INSERT INTO PhieuMuon (MaPM, SoTheDG, MaNV, NgayMuon, NgayHenTra) " +
                               "VALUES (@MaPM, @SoTheDG, @MaNV, @NgayMuon, @NgayHenTra)";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaPM", maPM);
                cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@NgayMuon", ngayMuon);
                cmd.Parameters.AddWithValue("@NgayHenTra", ngayHenTra);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }
        public void SuaPhieuMuon(int maPM, int soTheDG, int maNV, DateTime ngayHenTra)
        {
            try
            {
                MoKetNoi();
                //string query = @"UPDATE PhieuMuon SET SoTheDG = @SoTheDG, MaNV = @MaNV, 
                //               NgayMuon = @NgayMuon, 
                //               NgayHenTra = @NgayHenTra WHERE MaPM = @MaPM";
                string query = @"UPDATE PhieuMuon SET SoTheDG = @SoTheDG, MaNV = @MaNV, 
                               
                               NgayHenTra = @NgayHenTra WHERE MaPM = @MaPM";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaPM", maPM);
                cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
             //   cmd.Parameters.AddWithValue("@NgayMuon", ngayMuon);
                cmd.Parameters.AddWithValue("@NgayHenTra", ngayHenTra);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }
        public void XoaPhieuMuon(int maPM)
        {
            try
            {
                MoKetNoi();
                string queryChiTietPM = "DELETE FROM ChiTietPM WHERE MaPM = @maPM";
                SqlCommand cmdChiTietPM = new SqlCommand(queryChiTietPM, sqlCon);
                cmdChiTietPM.Parameters.AddWithValue("maPM", maPM);
                cmdChiTietPM.ExecuteNonQuery();

                string query = "DELETE FROM PhieuMuon WHERE MaPM = @MaPM";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@MaPM", maPM);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                DongKetNoi();
            }
        }
        public DataTable TimKiemPhieuMuon(string theDG, string maNV, string maPM)
        {
            MoKetNoi();
            string query = "SELECT MaPM, SoTheDG, MaNV , NgayMuon, NgayHenTra FROM PhieuMuon WHERE 1=1";

            if (!string.IsNullOrEmpty(theDG))
            {
                query += " AND SoTheDG LIKE @theDG";
            }
            if (!string.IsNullOrEmpty(maNV))
            {
                query += " AND MaNV LIKE @maNV";
            }
            if (!string.IsNullOrEmpty(maPM))
            {
                query += " AND MaPM LIKE @maPM";
            }
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            if (!string.IsNullOrEmpty(theDG))
            {
                cmd.Parameters.AddWithValue("@theDG", "%" + theDG + "%");
            }
            if (!string.IsNullOrEmpty(maNV))
            {
                cmd.Parameters.AddWithValue("@maNV", "%" + maNV + "%");
            }
            if (!string.IsNullOrEmpty(maPM))
            {
                cmd.Parameters.AddWithValue("@maPM", "%" + maPM + "%");
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtKetQuaTimKiem = new DataTable();
            adapter.Fill(dtKetQuaTimKiem);
            DongKetNoi();
            return dtKetQuaTimKiem;
        }
        public DateTime LayNgayHetHanThe(int soTheDG)
        {
            MoKetNoi();
            string query = "SELECT HanDung FROM TheDocGia WHERE SoTheDG = @SoTheDG";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
            SqlDataReader reader = cmd.ExecuteReader();
            DateTime han = DateTime.MinValue;
            if (reader.Read())
            {
                han = Convert.ToDateTime(reader["HanDung"]);
            }
            reader.Close();
            DongKetNoi();
            return han;
        }

        public bool KiemTraDocGiaDangMuonHoacQuaHan(int soTheDG)
        {
            MoKetNoi();
            string query = @"
        SELECT COUNT(*)
        FROM PhieuMuon pm
        INNER JOIN ChiTietPM ctp ON pm.MaPM = ctp.MaPM
        WHERE pm.SoTheDG = @SoTheDG
        AND (ctp.NgayTra IS NULL OR pm.NgayHenTra < GETDATE())";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);

            int soLuongMuon = (int)cmd.ExecuteScalar();
            DongKetNoi();

            return soLuongMuon > 0;
        }
        public DataTable LayThongTinBanDoc(int soTheDG)
        {
            MoKetNoi();
            string query = @"
    SELECT pm.MaPM, s.TenSach, ctp.TinhTrangKhiMuon, pm.NgayMuon, pm.NgayHenTra,
           CASE 
               WHEN ctp.TinhTrangTra IS NOT NULL THEN N'Đã trả'
               WHEN pm.NgayHenTra < GETDATE() THEN N'Quá hạn'
               ELSE N'Đang mượn' 
           END AS TinhTrangMuon
    FROM PhieuMuon pm
    INNER JOIN ChiTietPM ctp ON pm.MaPM = ctp.MaPM
    INNER JOIN Sach s ON ctp.MaSach = s.MaSach
    WHERE pm.SoTheDG = @SoTheDG";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@SoTheDG", soTheDG);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable result = new DataTable();
            adapter.Fill(result);
            DongKetNoi();

            return result;
        }
        public bool KiemTraPhieuMuonTonTai(int maPM)
        {
            MoKetNoi();
            string query = "SELECT COUNT(*) FROM PhieuMuon WHERE MaPM = @MaPM";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaPM", maPM);
            int count = (int)cmd.ExecuteScalar();
            DongKetNoi();
            return count > 0;
        }
        public DateTime LayNgayMuon(int maPM)
        {
            MoKetNoi();
            string query = "SELECT NgayMuon FROM PhieuMuon WHERE MaPM = @MaPM";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaPM", maPM);
            DateTime ngayMuon = DateTime.MinValue;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ngayMuon = Convert.ToDateTime(reader["NgayMuon"]);
            }
            reader.Close();
            DongKetNoi();

            return ngayMuon;
        }

        // form chi tiết phiếu mượn
        public DataTable LayChiTietPhieuMuon(string maPM)
        {
            MoKetNoi();
            string query = @"
        SELECT ctp.MaPM, ctp.MaSach, s.TenSach, ctp.TinhTrangKhiMuon
        FROM ChiTietPM ctp
        INNER JOIN Sach s ON ctp.MaSach = s.MaSach
        WHERE ctp.MaPM = @MaPM";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaPM", maPM);

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dr);
            DongKetNoi();
            return table;
        }
        public void ThemChiTietPhieuMuon(string maPM, string maSach, string tinhTrangKhiMuon)
            {
                MoKetNoi();
                try
                {
                    string updateBookQuery = "UPDATE Sach SET SoLuong = SoLuong - 1, SoLuongMuon = SoLuongMuon + 1 WHERE MaSach = @MaSach AND SoLuong > 0";
                    SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, sqlCon);
                    updateBookCmd.Parameters.AddWithValue("@MaSach", maSach);

                    int soDongAnhHuong = updateBookCmd.ExecuteNonQuery();
                    if (soDongAnhHuong == 0)
                    {
                        throw new Exception("Sách đã hết trong kho.");
                    }

                    string insertQuery = "INSERT INTO ChiTietPM (MaPM, MaSach, TinhTrangKhiMuon) VALUES (@MaPM, @MaSach, @TinhTrangKhiMuon)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, sqlCon);
                    insertCmd.Parameters.AddWithValue("@MaPM", maPM);
                    insertCmd.Parameters.AddWithValue("@MaSach", maSach);
                    insertCmd.Parameters.AddWithValue("@TinhTrangKhiMuon", tinhTrangKhiMuon);
                    insertCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm chi tiết phiếu mượn: " + ex.Message);
                }
                finally
                {
                    DongKetNoi();
                }
            }
        public void SuaChiTietPhieuMuon(string maPM, string maSach, string tinhTrangKhiMuon)
            {
                MoKetNoi();
                try
                {
                    string query = "UPDATE ChiTietPM SET TinhTrangKhiMuon = @TinhTrangKhiMuon WHERE MaPM = @MaPM AND MaSach = @MaSach";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.Parameters.AddWithValue("@MaPM", maPM);
                    cmd.Parameters.AddWithValue("@MaSach", maSach);
                    cmd.Parameters.AddWithValue("@TinhTrangKhiMuon", tinhTrangKhiMuon);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa chi tiết phiếu mượn: " + ex.Message);
                }
                finally
                {
                    DongKetNoi();
                }
            }
        public void XoaChiTietPhieuMuon(string maPM, string maSach)
            {
                MoKetNoi();
                try
                {
                    string deleteQuery = "DELETE FROM ChiTietPM WHERE MaPM = @MaPM AND MaSach = @MaSach";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, sqlCon);
                    deleteCmd.Parameters.AddWithValue("@MaPM", maPM);
                    deleteCmd.Parameters.AddWithValue("@MaSach", maSach);
                    deleteCmd.ExecuteNonQuery();

                    string updateBookQuery = "UPDATE Sach SET SoLuong = SoLuong + 1, SoLuongMuon = SoLuongMuon - 1 WHERE MaSach = @MaSach";
                    SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, sqlCon);
                    updateBookCmd.Parameters.AddWithValue("@MaSach", maSach);
                    updateBookCmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa chi tiết phiếu mượn: " + ex.Message);
                }
                finally
                {
                    DongKetNoi();
               }
            }
        public DataTable LayTatCaTenSach()
        {
            MoKetNoi();
            string query = "SELECT MaSach, CAST(MaSach AS nvarchar(10)) + ' - ' + TenSach AS DisplayText FROM Sach";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtTenSach = new DataTable();
            adapter.Fill(dtTenSach);
            DongKetNoi();
            return dtTenSach;
        }
        public int DemSoSachMuon(string maPM)
        {
            MoKetNoi();
            int soSach = 0;
            string query = "SELECT COUNT(*) FROM ChiTietPM WHERE MaPM = @maPM";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@maPM", maPM);
               soSach = (int)cmd.ExecuteScalar();
        
            DongKetNoi() ;
            return soSach;
        }
        // form trả sách
        public DataTable LayThongTinTraSach()
        {
            MoKetNoi();
            string query = @"SELECT pm.MaPM, dg.HoTenDG, ctp.MaSach, s.TenSach, pm.NgayMuon, pm.NgayHenTra, ctp.NgayTra, 
                            CASE WHEN ctp.TrangThai = 1 THEN N'Đã trả' ELSE N'Chưa trả' END AS TrangThai , ctp.TinhTrangTra
                     FROM PhieuMuon pm
                     INNER JOIN ChiTietPM ctp ON pm.MaPM = ctp.MaPM
                     INNER JOIN Sach s ON ctp.MaSach = s.MaSach
                     INNER JOIN TheDocGia tdg ON pm.SoTheDG = tdg.SoTheDG
                     INNER JOIN DocGia dg ON tdg.MaDG = dg.MaDG";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtThongTinTraSach = new DataTable();
            adapter.Fill(dtThongTinTraSach);
            DongKetNoi();
            return dtThongTinTraSach;
        }
        public void CapNhatTrangThaiTraSach(string maPM, string maSach, string tinhTrangTra)
        {
            MoKetNoi();
            string query1 = "UPDATE ChiTietPM SET TrangThai = @TrangThai, NgayTra = @NgayTra, TinhTrangTra = @TinhTrangTra WHERE MaPM = @MaPM AND MaSach = @MaSach";
            SqlCommand cmd1 = new SqlCommand(query1, sqlCon);
            cmd1.Parameters.AddWithValue("@TrangThai", 1);
            cmd1.Parameters.AddWithValue("@MaPM", maPM);
            cmd1.Parameters.AddWithValue("@MaSach", maSach);
            cmd1.Parameters.AddWithValue("@NgayTra", DateTime.Now);
            cmd1.Parameters.AddWithValue("@TinhTrangTra", tinhTrangTra);
            cmd1.ExecuteNonQuery();

            string query2 = "UPDATE Sach SET SoLuong = SoLuong + 1, SoLuongMuon = SoLuongMuon - 1 WHERE MaSach = @MaSach";
            SqlCommand cmd2 = new SqlCommand(query2, sqlCon);
            cmd2.Parameters.AddWithValue("@MaSach", maSach);
            cmd2.ExecuteNonQuery();

            DongKetNoi();
        }
        public DataTable TimKiemPhieuTra(string maPhieuMuon)
        {
            MoKetNoi();
            string query = @"SELECT pm.MaPM, dg.HoTenDG, ctp.MaSach,s.TenSach, pm.NgayMuon, pm.NgayHenTra, ctp.NgayTra, CASE 
        WHEN ctp.TrangThai = 1 
        THEN N'Đã trả' ELSE N'Chưa trả' END AS TrangThai, ctp.TinhTrangTra
        FROM 
            PhieuMuon pm
        INNER JOIN 
            ChiTietPM ctp ON pm.MaPM = ctp.MaPM
        INNER JOIN 
            Sach s ON ctp.MaSach = s.MaSach
        INNER JOIN 
            TheDocGia tdg ON pm.SoTheDG = tdg.SoTheDG
        INNER JOIN 
            DocGia dg ON tdg.MaDG = dg.MaDG
        WHERE 
            pm.MaPM = @MaPM";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaPM", maPhieuMuon);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtKetQuaTimKiem = new DataTable();
            adapter.Fill(dtKetQuaTimKiem);

            DongKetNoi();
            return dtKetQuaTimKiem;
        }
        public bool LayTrangThaiPhieuMuon(string maPM, string maSach)
        {
            MoKetNoi();
            string query = "SELECT TrangThai FROM ChiTietPM WHERE MaPM = @MaPM AND MaSach = @MaSach";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@MaPM", maPM);
            cmd.Parameters.AddWithValue("@MaSach", maSach);

            object result = cmd.ExecuteScalar();
            DongKetNoi();

            return result != DBNull.Value && Convert.ToInt32(result) == 1;
        }


        // form xử lý phạt
        public DataTable dsHoaDonPhat()
        {
            MoKetNoi();
            string query = "SELECT * FROM dbo.HoaDonPhat";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dr);
            DongKetNoi();
            return table;
        }
        public void themHoaDonPhat(string soHoaDon, string soTheDG, string maNV, string tienphat, string lydo, DateTime ngayphat, string hinhthuc)
        {
            MoKetNoi();
            string query = "INSERT INTO dbo.HoaDonPhat (SoHoaDon, SoTheDG, MaNV, Tienphat, LyDo, HinhThuc, NgayPhat)" +
                           "VALUES (@soHoaDon, @soTheDG, @maNV, @tienphat, @lydo, @hinhthuc, @ngayphat)";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("soHoaDon", soHoaDon);
            cmd.Parameters.AddWithValue("soTheDG", soTheDG);
            cmd.Parameters.AddWithValue("maNV", maNV);
            cmd.Parameters.AddWithValue("tienphat", tienphat);
            cmd.Parameters.AddWithValue("lydo", lydo);
            cmd.Parameters.AddWithValue("ngayphat", ngayphat);
            cmd.Parameters.AddWithValue("hinhthuc", hinhthuc);
            cmd.ExecuteNonQuery();
            DongKetNoi();
        }
        public void suaHoaDonPhat(string soHD, string soTheDG, string maNV, string tienphat, string lydo, DateTime ngayphat, string hinhthuc)
        {
            MoKetNoi();
            string query = "UPDATE dbo.HoaDonPhat SET  SoTheDG = @soTheDG, MaNV = @maNV, TienPhat = @tienphat, LyDo = @lydo, NgayPhat = @ngayphat, HinhThuc = @hinhthuc WHERE SoHoaDon = @soHD";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("soHD", soHD);
            cmd.Parameters.AddWithValue("soTheDG", soTheDG);
            cmd.Parameters.AddWithValue("maNV", maNV);
            cmd.Parameters.AddWithValue("tienphat", tienphat);
            cmd.Parameters.AddWithValue("lydo", lydo);
            cmd.Parameters.AddWithValue("ngayphat", ngayphat);
            cmd.Parameters.AddWithValue("hinhthuc", hinhthuc);
            cmd.ExecuteNonQuery();
            DongKetNoi();
        }
        public void xoaHoaDonPhat(string soHD)
        {
            MoKetNoi();
            string query = "DELETE FROM dbo.HoaDonPhat WHERE SoHoaDon = @soHD";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("soHD", soHD);
            cmd.ExecuteNonQuery();
            DongKetNoi();
        }
        public DataTable timKiemHoaDonPhat(string soHoaDon, string soTheDG, string maNV)
        {
            MoKetNoi();
            string query = "SELECT * FROM dbo.HoaDonPhat WHERE 1 = 1";
            if (!string.IsNullOrEmpty(soHoaDon))
            {
                query += " AND SoHoaDon = @soHoaDon";
            }
            if (!string.IsNullOrEmpty(soTheDG))
            {
                query += " AND SoTheDG = @soTheDG";
            }
            if (!string.IsNullOrEmpty(maNV))
            {
                query += " AND MaNV = @maNV";
            }
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            if (!string.IsNullOrEmpty(soHoaDon))
            {
                cmd.Parameters.AddWithValue("soHoaDon", soHoaDon);
            }
            if (!string.IsNullOrEmpty(soTheDG))
            {
                cmd.Parameters.AddWithValue("soTheDG", soTheDG);
            }
            if (!string.IsNullOrEmpty(maNV))
            {
                cmd.Parameters.AddWithValue("maNV", maNV);
            }
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dr);
            DongKetNoi();
            return table;
        }
        public bool tontaihoadon(string soHoaDon)
        {
            try
            {                MoKetNoi();
                string query = "SELECT COUNT(*) FROM HoaDonPhat WHERE SoHoaDon = @SoHoaDon ";
                SqlCommand cmd = new SqlCommand(query, sqlCon);
                cmd.Parameters.AddWithValue("@SoHoaDon", soHoaDon);

                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
            finally
            {
                DongKetNoi();
            }
        }


        // form the
        public DataTable LayThongTinTheDG(int soTheDG)
        {
            MoKetNoi();
            string query = "SELECT SoTheDG, MaDG, NgayCap, HanDung FROM TheDocGia WHERE SoTheDG = @soTheDG";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("soTheDG", soTheDG);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dr);
            DongKetNoi();
            return table;
        }

        // thống kê
        public DataTable LayLuotMuonSach()
        {
            MoKetNoi();
            string query = @"SELECT pm.MaPM, pm.SoTheDG, pm.NgayMuon, pm.NgayHenTra, pm.NgayTra, 
                     sach.TenSach, dg.HoTenDG 
                     FROM PhieuMuon pm
                     INNER JOIN ChiTietPM ctp ON pm.MaPM = ctp.MaPM
                     INNER JOIN Sach sach ON ctp.MaSach = sach.MaSach
                     INNER JOIN TheDocGia tdg ON pm.SoTheDG = tdg.SoTheDG
                     INNER JOIN DocGia dg ON tdg.MaDG = dg.MaDG";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtLuotMuonSach = new DataTable();
            adapter.Fill(dtLuotMuonSach);
            DongKetNoi();
            return dtLuotMuonSach;
        }
        public DataTable LaySachBiThatLac()
        {
            MoKetNoi();
            string query = @"
        SELECT s.MaSach, s.TenSach, s.NXB, s.TacGia
        FROM Sach s
        INNER JOIN ChiTietPM ctp ON s.MaSach = ctp.MaSach
        INNER JOIN HoaDonPhat hdp ON ctp.MaPM = hdp.SoHoaDon
        WHERE hdp.LyDo = N'Mất sách'";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtSachThatLac = new DataTable();
            adapter.Fill(dtSachThatLac);
            DongKetNoi();
            return dtSachThatLac;
        }
        public DataTable LaySachCanThanhLy()
        {
            MoKetNoi();
            string query = @"SELECT MaSach, TenSach, NXB, TacGia, ViTri, SoLuong, TinhTrang 
                     FROM Sach
                     WHERE TinhTrang = N'Cũ'";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtSachCanThanhLy = new DataTable();
            adapter.Fill(dtSachCanThanhLy);
            DongKetNoi();
            return dtSachCanThanhLy;
        }
        public DataTable LaySachQuaHan()
        {
            MoKetNoi();
            string query = @"SELECT 
    pm.MaPM, 
    dg.HoTenDG, 
    s.TenSach, 
    pm.NgayMuon, 
    pm.NgayHenTra, 
    DATEDIFF(DAY, pm.NgayHenTra, GETDATE()) AS SoNgayQuaHan
FROM 
    PhieuMuon pm
INNER JOIN 
    ChiTietPM ctp ON pm.MaPM = ctp.MaPM
INNER JOIN 
    Sach s ON ctp.MaSach = s.MaSach
INNER JOIN 
    TheDocGia tdg ON pm.SoTheDG = tdg.SoTheDG
INNER JOIN 
    DocGia dg ON tdg.MaDG = dg.MaDG
WHERE 
    pm.NgayHenTra < GETDATE() AND 
    ctp.NgayTra IS NULL";

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtSachQuaHan = new DataTable();
            adapter.Fill(dtSachQuaHan);
            DongKetNoi();
            return dtSachQuaHan;
        }


        // tìm kiếm của cacs form thống kê
        public DataTable LayLuotMuonSach(string maPM , string tenSach , string hoTenDG )
        {
            MoKetNoi();
            string query = @"SELECT pm.MaPM, pm.SoTheDG, pm.NgayMuon, pm.NgayHenTra, pm.NgayTra, 
                     sach.TenSach, dg.HoTenDG 
                     FROM PhieuMuon pm
                     INNER JOIN ChiTietPM ctp ON pm.MaPM = ctp.MaPM
                     INNER JOIN Sach sach ON ctp.MaSach = sach.MaSach
                     INNER JOIN TheDocGia tdg ON pm.SoTheDG = tdg.SoTheDG
                     INNER JOIN DocGia dg ON tdg.MaDG = dg.MaDG
                     WHERE 1 = 1";

            if (!string.IsNullOrEmpty(maPM))
            {
                query += " AND pm.MaPM LIKE @maPM";
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                query += " AND sach.TenSach LIKE @tenSach";
            }
            if (!string.IsNullOrEmpty(hoTenDG))
            {
                query += " AND dg.HoTenDG LIKE @hoTenDG";
            }

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            if (!string.IsNullOrEmpty(maPM))
            {
                cmd.Parameters.AddWithValue("@maPM", "%" + maPM + "%");
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                cmd.Parameters.AddWithValue("@tenSach", "%" + tenSach + "%");
            }
            if (!string.IsNullOrEmpty(hoTenDG))
            {
                cmd.Parameters.AddWithValue("@hoTenDG", "%" + hoTenDG + "%");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtLuotMuonSach = new DataTable();
            adapter.Fill(dtLuotMuonSach);
            DongKetNoi();
            return dtLuotMuonSach;
        }

        public DataTable LaySachBiThatLac(string maSach , string tenSach )
        {
            MoKetNoi();
            string query = @"SELECT s.MaSach, s.TenSach, s.NXB, s.TacGia
        FROM Sach s
        INNER JOIN ChiTietPM ctp ON s.MaSach = ctp.MaSach
        INNER JOIN HoaDonPhat hdp ON ctp.MaPM = hdp.SoHoaDon
        WHERE hdp.LyDo = N'Mất sách'";

            if (!string.IsNullOrEmpty(maSach))
            {
                query += " AND s.MaSach LIKE @maSach";
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                query += " AND s.TenSach LIKE @tenSach";
            }

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            if (!string.IsNullOrEmpty(maSach))
            {
                cmd.Parameters.AddWithValue("@maSach", "%" + maSach + "%");
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                cmd.Parameters.AddWithValue("@tenSach", "%" + tenSach + "%");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtSachThatLac = new DataTable();
            adapter.Fill(dtSachThatLac);
            DongKetNoi();
            return dtSachThatLac;
        }

        public DataTable LaySachCanThanhLy(string maSach, string tenSach )
        {
            MoKetNoi();
            string query = @"SELECT MaSach, TenSach, NXB, TacGia, ViTri, SoLuong, TinhTrang 
                     FROM Sach
                     WHERE TinhTrang = N'Cũ'";

            if (!string.IsNullOrEmpty(maSach))
            {
                query += " AND MaSach LIKE @maSach";
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                query += " AND TenSach LIKE @tenSach";
            }

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            if (!string.IsNullOrEmpty(maSach))
            {
                cmd.Parameters.AddWithValue("@maSach", "%" + maSach + "%");
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                cmd.Parameters.AddWithValue("@tenSach", "%" + tenSach + "%");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtSachCanThanhLy = new DataTable();
            adapter.Fill(dtSachCanThanhLy);
            DongKetNoi();
            return dtSachCanThanhLy;
        }

        public DataTable LaySachQuaHan(string maPM , string tenSach , string hoTenDG )
        {
            MoKetNoi();
            string query = @"SELECT 
    pm.MaPM, 
    dg.HoTenDG, 
    s.TenSach, 
    pm.NgayMuon, 
    pm.NgayHenTra, 
    DATEDIFF(DAY, pm.NgayHenTra, GETDATE()) AS SoNgayQuaHan
FROM 
    PhieuMuon pm
INNER JOIN 
    ChiTietPM ctp ON pm.MaPM = ctp.MaPM
INNER JOIN 
    Sach s ON ctp.MaSach = s.MaSach
INNER JOIN 
    TheDocGia tdg ON pm.SoTheDG = tdg.SoTheDG
INNER JOIN 
    DocGia dg ON tdg.MaDG = dg.MaDG
WHERE 
    pm.NgayHenTra < GETDATE() AND 
    ctp.NgayTra IS NULL";

            if (!string.IsNullOrEmpty(maPM))
            {
                query += " AND pm.MaPM LIKE @maPM";
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                query += " AND s.TenSach LIKE @tenSach";
            }
            if (!string.IsNullOrEmpty(hoTenDG))
            {
                query += " AND dg.HoTenDG LIKE @hoTenDG";
            }

            SqlCommand cmd = new SqlCommand(query, sqlCon);
            if (!string.IsNullOrEmpty(maPM))
            {
                cmd.Parameters.AddWithValue("@maPM", "%" + maPM + "%");
            }
            if (!string.IsNullOrEmpty(tenSach))
            {
                cmd.Parameters.AddWithValue("@tenSach", "%" + tenSach + "%");
            }
            if (!string.IsNullOrEmpty(hoTenDG))
            {
                cmd.Parameters.AddWithValue("@hoTenDG", "%" + hoTenDG + "%");
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtSachQuaHan = new DataTable();
            adapter.Fill(dtSachQuaHan);
            DongKetNoi();
            return dtSachQuaHan;
        }




    }
}
