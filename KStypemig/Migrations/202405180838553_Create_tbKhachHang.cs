namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_tbKhachHang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        ID_KhachHang = c.String(nullable: false, maxLength: 20),
                        TenTaiKhoan = c.String(maxLength: 20),
                        MatKhau = c.String(maxLength: 20),
                        HoTen = c.String(maxLength: 30),
                        SoDienThoai = c.String(maxLength: 20),
                        Email = c.String(maxLength: 35),
                        CCCD = c.String(maxLength: 12),
                    })
                .PrimaryKey(t => t.ID_KhachHang);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KhachHangs");
        }
    }
}
