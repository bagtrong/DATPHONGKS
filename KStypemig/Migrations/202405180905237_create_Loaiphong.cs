namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_Loaiphong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoaiPhongs",
                c => new
                    {
                        MaLoai = c.String(nullable: false, maxLength: 10),
                        TenLoai = c.String(maxLength: 30),
                        GhiChu = c.String(maxLength: 250),
                        DuongDanAnh = c.String(maxLength: 50),
                        SoluongPhong = c.Int(),
                    })
                .PrimaryKey(t => t.MaLoai);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoaiPhongs");
        }
    }
}
