using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace KStypemig.Models
{
    public class QLKSpart4Context : DbContext
    {
        public QLKSpart4Context():base("MyConnectionStringDB")
        {

        }
        public DbSet<ThongKe> thongKes { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<NhanVien> nhanViens { get; set;}

        public DbSet<KhachHang> khachHangs { get; set; }
        public DbSet<DichVu> dichVus { get; set; }
        public DbSet<Phong> phongs { get; set; }
        public DbSet<AnhChiTietPhong> anhChiTietPhongs { get; set; }
        public DbSet<LoaiPhong> loaiPhongs { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<DatPhong> datPhongs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DichVu>()
                .Property(p => p.GiaDichVu)
                .HasColumnType("decimal")
                .HasPrecision(18, 2); // Đảm bảo bạn chỉ định độ chính xác và thang đo chính xác

            modelBuilder.Entity<DatPhong>()
                .Property(p => p.ThanhTien)
                .HasColumnType("decimal")
                .HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }

    }
}