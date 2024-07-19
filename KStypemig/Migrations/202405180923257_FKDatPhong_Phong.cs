namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKDatPhong_Phong : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DatPhongs", "MaPhong");
            AddForeignKey("dbo.DatPhongs", "MaPhong", "dbo.Phong", "MaPhong");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatPhongs", "MaPhong", "dbo.Phong");
            DropIndex("dbo.DatPhongs", new[] { "MaPhong" });
        }
    }
}
