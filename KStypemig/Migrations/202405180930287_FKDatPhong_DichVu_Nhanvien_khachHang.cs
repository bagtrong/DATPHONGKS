namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKDatPhong_DichVu_Nhanvien_khachHang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DatPhongs", "dichvus_MaDichVu", c => c.String(maxLength: 10));
            CreateIndex("dbo.DatPhongs", "ID_KhachHang");
            CreateIndex("dbo.DatPhongs", "ID_NhanVien");
            CreateIndex("dbo.DatPhongs", "dichvus_MaDichVu");
            AddForeignKey("dbo.DatPhongs", "dichvus_MaDichVu", "dbo.DichVus", "MaDichVu");
            AddForeignKey("dbo.DatPhongs", "ID_KhachHang", "dbo.KhachHangs", "ID_KhachHang");
            AddForeignKey("dbo.DatPhongs", "ID_NhanVien", "dbo.NhanViens", "ID_NhanVien");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatPhongs", "ID_NhanVien", "dbo.NhanViens");
            DropForeignKey("dbo.DatPhongs", "ID_KhachHang", "dbo.KhachHangs");
            DropForeignKey("dbo.DatPhongs", "dichvus_MaDichVu", "dbo.DichVus");
            DropIndex("dbo.DatPhongs", new[] { "dichvus_MaDichVu" });
            DropIndex("dbo.DatPhongs", new[] { "ID_NhanVien" });
            DropIndex("dbo.DatPhongs", new[] { "ID_KhachHang" });
            DropColumn("dbo.DatPhongs", "dichvus_MaDichVu");
        }
    }
}
