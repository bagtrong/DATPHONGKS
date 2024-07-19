namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_DatPhong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatPhongs",
                c => new
                    {
                        MaDatPhong = c.Int(nullable: false, identity: true),
                        ID_KhachHang = c.String(maxLength: 20),
                        TenKhachHang = c.String(maxLength: 30),
                        cancuoccd = c.String(maxLength: 12),
                        ID_NhanVien = c.String(maxLength: 20),
                        MaPhong = c.String(maxLength: 10),
                        NgayDat = c.DateTime(storeType: "date"),
                        NgayDenDuDinh = c.DateTime(storeType: "date"),
                        NgayTraDuDinh = c.DateTime(storeType: "date"),
                        NgayDenthucte = c.DateTime(storeType: "date"),
                        NgayTrathucte = c.DateTime(storeType: "date"),
                        thoigianthanhtoan = c.DateTime(storeType: "date"),
                        tinhtrangthanhtoan = c.String(maxLength: 20),
                        phuongthucthanhtoan = c.String(maxLength: 20),
                        DichVu = c.String(maxLength: 20),
                        ThanhTien = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaDatPhong);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DatPhongs");
        }
    }
}
