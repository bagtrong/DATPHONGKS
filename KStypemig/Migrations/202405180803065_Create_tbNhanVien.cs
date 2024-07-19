namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_tbNhanVien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NhanViens",
                c => new
                    {
                        ID_NhanVien = c.String(nullable: false, maxLength: 20),
                        MatKhau = c.String(maxLength: 20),
                        HoTen = c.String(maxLength: 30),
                        SoDienThoai = c.String(maxLength: 20),
                        Email = c.String(maxLength: 35),
                        LaAdmin = c.Boolean(),
                        CCCD = c.String(maxLength: 12),
                        vaitro = c.String(maxLength: 12),
                    })
                .PrimaryKey(t => t.ID_NhanVien);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NhanViens");
        }
    }
}
