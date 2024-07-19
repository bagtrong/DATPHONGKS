namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_Phong_LoaiPh_AnhChitietP : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AnhChiTietPhongs", "maphong");
            CreateIndex("dbo.Phong", "MaLoai");
            AddForeignKey("dbo.AnhChiTietPhongs", "maphong", "dbo.Phong", "MaPhong");
            AddForeignKey("dbo.Phong", "MaLoai", "dbo.LoaiPhongs", "MaLoai");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phong", "MaLoai", "dbo.LoaiPhongs");
            DropForeignKey("dbo.AnhChiTietPhongs", "maphong", "dbo.Phong");
            DropIndex("dbo.Phong", new[] { "MaLoai" });
            DropIndex("dbo.AnhChiTietPhongs", new[] { "maphong" });
        }
    }
}
