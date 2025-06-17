

create table DocGia (
		MaDG int PRIMARY KEY ,
		CCCD varchar(12),
		HoTenDG nvarchar(50),
		GioiTinh nvarchar (10),
		NgaySinh date,
		SDT varchar(10),
		Email nvarchar (50),
		DiaChi nvarchar (max),
		)
go
INSERT INTO DocGia (MaDG,CCCD, HoTenDG, GioiTinh, NgaySinh ,SDT,Email, DiaChi)
VALUES
(211031001,211031001, N'Ninh Dương Lan Ngọc',N'Nữ', '2003-01-03' , '0987654321', N'ndlngoc0301@gmail.com',N'Kim Thành, Hải Dương'),
(211031002,211031002, N'Lê Dương Bảo Lâm', N'Nam', '2003-02-05', '0965432187', N'ldblam0502@gmail.com', N'Thanh Xuân, Hà Nội'),
(211031003,211031003, N'Trần Thị Cẩm', N'Nữ', '2003-03-07', '0943218765', N'ttcam0703@gmail.com', N'Hoàng Mai, Hà Nội'),
(211031004,211031004,N'Đỗ Văn Dũng', N'Nam', '2003-04-09', '0921876543', N'dvdung0904@gmail.com', N'Thanh Xuân, Hà Nội'),
(211031005,211031005, N'Vũ Hà Vân Anh', N'Nữ', '2003-05-11', '0907654321', N'vhvanh1105@gmail.com', N'Hoàng Mai, Hà Nội'),
(211031006,211031006, N'Lý Văn Gia', N'Nam', '2003-06-13', '0896543218', N'lvgia1306@gmail.com', N'Thanh Xuân, Hà Nội'),
(211031007,211031007, N'Hoàng Thị Hà', N'Nữ', '2003-07-15', '0887654321', N'htha1507@gmail.com', N'Hoàng Mai, Hà Nội'),
(211031008,211031008, N'Vũ Văn Khánh', N'Nam', '2003-08-17', '0876543219', N'vvkhanh1708@gmail.com', N'Hoàng Mai, Hà Nội'),
(211031009,211031009, N'Nguyễn Thùy Dung', N'Nữ', '2003-09-19', '0865432198', N'ntdung1909@gmail.com', N'Thanh Xuân, Hà Nội'),
(211031010,211031010, N'Võ Vũ Trường Giang', N'Nam', '2004-01-01', '0965432198', N'vvtgiang01@gmail.com', N'Đông Anh, Hà Nội'),
(211031011,211031011, N'Khổng Tú Quỳnh', N'Nữ', '2004-02-03', '0854321987', N'ktquynh02@gmail.com', N'Đông Anh, Hà Nội'),
(211031012,211031012, N'Phạm Văn Cường', N'Nam', '2004-03-05', '0943219876', N'pvcuong03@gmail.com', N'Thanh Xuân, Hà Nội');
go
create table NhanVien (
		MaNV  int PRIMARY KEY,
		HoTenNV nvarchar(50),
		GioiTinh nvarchar (10),
		NgaySinh date,
		SDT varchar(10),
		Email nvarchar(50),
		MatKhau nvarchar (50)
		)
go  
	insert into  NhanVien
	values (211001, N'Vũ Hà Trang ', N'Nữ', '2003-10-12', '0375995947', 'vhtrang@gmail.com', 'trang211001');
	insert into  NhanVien  
	values (211002,	N'Phạm Đức Anh', N'Nam', '2003-04-13', '0974829461', 'pdanh@gmail.com', 'phamdanh211002');
	insert into  NhanVien 
	values (211003, N'Trần Văn Cương', N'Nam', '2003-09-06', '0965589805', 'tvcuong@gmail.com', 'tvcuong211003');
	insert into  NhanVien  
	values (211004, N'Dương Quốc Cường ', N'Nam', '2003-03-26', '0357049001', 'dqcuong@gmail.com', 'dqcuong211004');
	insert into NhanVien
	values (211005, N'Nguyễn Hiếu Đạt', N'Nam', '2003-07-05', '0369523946', 'nhdat@gmail.com', 'nhdat211005');
	go

create table LoaiSach(
		MaLoaiSach int PRIMARY KEY,
		TenLoaiSach nvarchar(100)
) 
go
INSERT INTO LoaiSach (MaLoaiSach, TenLoaiSach)
VALUES
(1, N'Tài liệu'),
(2, N'Sách ngoại văn'),
(3, N'Giáo trình'),
(4, N'Sách chuyên khảo'),
(5, N'Chính trị - pháp luật');

go 
create table Sach(
		MaSach int  PRIMARY KEY,
		TenSach nvarchar(max), 
		NXB nvarchar (100),
		TacGia nvarchar(100),
		MaLoaiSach int  FOREIGN KEY REFERENCES LoaiSach(MaLoaiSach),
		ViTri nvarchar(20),
		SoLuongMuon int,
		SoLuong int,
		TinhTrang nvarchar(max)
		)
go
INSERT INTO Sach (MaSach, TenSach, NXB,TacGia, MaLoaiSach, ViTri, SoLuongMuon, SoLuong, TinhTrang )
VALUES
('1001', N'English Grammar in Use', N'Cambridge University Press', N'Raymond Murphy', '1',N'KỆ 1', '1', '8', N'Mới'),
('1002', N'Giải tích 1', N'Nhà xuất bản Đại học Quốc gia Hà Nội', N'Nguyễn Đình Trí', '1',N'KỆ 1', '1', '2', N'Mới'),
('1003', N'Lập trình Python', N'Nhà xuất bản Thanh Niên', N'Nguyễn Tấn Trần Minh Khang', '1',N'KỆ 1', '1', '3', N'Mới'),
('2001', N'The Catcher in the Rye', N'Brown and Company', N'J.D. Salinger - Little', '2',N'KỆ 2', '0', '9', N'Cũ'),
('2002', N'Le Petit Prince', N'Gallimard', N'Antoine de Saint-Exupéry', '2', N'KỆ 2', '1', '18', N'Mới'),
('3001', N'Giáo trình Kinh tế học vi mô', N'Nhà xuất bản Đại học Kinh tế Quốc dân', N'Nguyễn Thị Hồng Minh', '3',N'KỆ 3', '1', '11', N'Mới'),
('3002', N'Giáo trình Cơ sở dữ liệu', N'Nhà xuất bản Đại học Bách khoa Hà Nội', N'Trần Đình Khang', '3', N'KỆ 3', '1', '9', N'Cũ'),
('3003', N'Giáo trình Luật hình sự', N'Nhà xuất bản Đại học Luật Hà Nội', N'Nguyễn Văn Quang', '3', N'KỆ 3', '0', '3', N'Cũ'),
('4001', N'Lịch sử Việt Nam từ nguồn gốc đến giữa thế kỷ XIX', N'Nhà xuất bản Khoa học Xã hội', N'Phạm Văn Sơn', '4', N'KỆ 4', '0', '12', N'Mới'),
('4002', N'Văn học Việt Nam dưới chế độ phong kiến', N'Nhà xuất bản Văn học', N'Nguyễn Đình Đầu', '4', N'KỆ 4', '0', '10', N'Mới'),
('5001', N'Hiến Pháp Nước Cộng Hòa Xã Hội Chủ Nghĩa Việt Nam', N'Chính Trị Quốc Gia Sự Thật', N'Quốc Hội', '5', N'KỆ 5', '1', '5', N'Mới'),
('5002', N'Hiến Pháp Nước Cộng Hòa Xã Hội Chủ Nghĩa Việt Nam', N'Chính Trị Quốc Gia Sự Thật', N'Quốc Hội', '5', N'KỆ 5', '1', '5', N'Mới'),
('5003', N'Pháp Lý M&A Căn Bản', N'Công Thương', N'Luật sư Trương Hữu Ngữ', '5', N'KỆ 5', '0', '5', N'Mới');

go 
create table TheDocGia(
		SoTheDG int PRIMARY KEY,
		MaDG int FOREIGN KEY REFERENCES DocGia(MaDG),
		NgayCap date,
		HanDung date  )
go
INSERT INTO TheDocGia(SoTheDG, MaDG, NgayCap, HanDung)
VALUES 
(1, 211031001, '2023-01-01', '2027-01-01'),
(2, 211031002, '2023-01-02', '2027-01-02'),
(3, 211031003, '2023-01-03', '2027-01-03'),
(4, 211031004, '2023-01-04', '2027-01-04'),
(5, 211031005, '2023-01-05', '2027-01-05'),
(6, 211031006, '2023-01-06', '2027-01-06'),
(7, 211031007, '2023-01-07', '2027-01-07'),
(8, 211031008, '2023-01-08', '2027-01-08'),
(9, 211031009, '2023-01-09', '2027-01-09'),
(10, 211031010, '2023-01-10', '2027-01-10'),
(11, 211031011, '2023-01-11', '2027-01-11'),
(12, 211031012, '2023-01-12', '2027-01-12'); 
go
create table PhieuMuon(
		MaPM int  PRIMARY KEY,
		SoTheDG int FOREIGN KEY REFERENCES TheDocGia(SoTheDG),
		MaNV int FOREIGN KEY REFERENCES NhanVien(MaNV),
		NgayMuon date,
		NgayHenTra date,
		NgayTra date,
		TrangThai bit,
		)
go
INSERT INTO PhieuMuon
VALUES (1,  10, 211001, '2023-01-01', '2023-02-01', NULL, 0);
INSERT INTO PhieuMuon
VALUES (2, 11, 211002, '2023-01-02', '2023-02-02', NULL, 0);
INSERT INTO PhieuMuon
VALUES (3, 12, 211003, '2023-01-04', '2023-02-04', NULL, 0);
INSERT INTO PhieuMuon
VALUES (4, 1, 211003, '2023-01-05', '2023-01-19','2023-02-02', 1);
go
create table ChiTietPM (
		MaPM int  FOREIGN KEY REFERENCES PhieuMuon(MaPM),
		MaSach int FOREIGN KEY REFERENCES Sach(MaSach),
		TinhTrangKhiMuon nvarchar(500),
		TrangThai bit,
		NgayTra date,
		TinhTrangTra nvarchar(500),
		CONSTRAINT ChiTietPM_PK  PRIMARY KEY (MaPM, MaSach)
		)
go
INSERT INTO ChiTietPM(MaPM, MaSach, TinhTrangKhiMuon,TrangThai,NgayTra)
VALUES 
(1, 1001, N'Mới',0, NULL),
(1, 1003, N'Mới',0, NULL),
(2, 2002, N'Mới',0, NULL),
(3, 1002, N'Mới',0, NULL),
(3, 3001, N'Mới',0, NULL),
(3, 3002, N'Cũ',0, NULL),
(4, 5001, N'Mới',0, NULL),
(4, 5002, N'Mới',0, NULL)
go
create table HoaDonPhat(
		SoHoaDon int PRIMARY KEY,
		SoTheDG int FOREIGN KEY REFERENCES TheDocGia(SoTheDG),
		MaNV int FOREIGN KEY REFERENCES NhanVien(MaNV),
		Tienphat int,
		LyDo nvarchar(max),
		HinhThuc nvarchar(max),
		NgayPhat date)
go
INSERT INTO HoaDonPhat(SoHoaDon, SoTheDG, MaNV, HinhThuc, Tienphat, LyDo, NgayPhat)
VALUES 
(1, 10, 211001,N' nhắc nhở ', 0,  N'Quá hạn mượn sách', '2023-02-02'),
(2, 11, 211002,N'phạt tiền', 90000, N'Mất sách', '2023-02-10'),
(3, 12, 211003,N'phạt tiền ', 70000, N'Hỏng sách', '2023-02-17');
go 

