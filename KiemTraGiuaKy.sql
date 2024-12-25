create database DSSinhVien

use DSSinhVien
go

create table Lop(
MaLop char(3) primary key not null,
TenLop nvarchar(30) not null,
)
create table SinhVien(
MaSV char(6) primary key not null,
HoTenSV nvarchar(30) not null,
NgaySinh datetime not null,
MaLop char(3) not null,
CONSTRAINT FK_SinhVien_Lop FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
)