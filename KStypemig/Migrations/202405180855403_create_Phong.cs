namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_Phong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phong",
                c => new
                    {
                        MaPhong = c.String(nullable: false, maxLength: 10),
                        TenPhong = c.String(maxLength: 10),
                        MaLoai = c.String(maxLength: 10),
                        DienTich = c.Int(),
                        GiaThue = c.Int(),
                        sogiuong = c.Int(),
                        ghichu = c.String(maxLength: 100),
                        tinhtrangphong = c.String(maxLength: 10),
                        Songuoitoida = c.Int(),
                    })
                .PrimaryKey(t => t.MaPhong);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Phong");
        }
    }
}
