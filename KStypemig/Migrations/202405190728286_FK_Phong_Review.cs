namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_Phong_Review : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "PhongId", c => c.String(maxLength: 10));
            CreateIndex("dbo.Reviews", "PhongId");
            AddForeignKey("dbo.Reviews", "PhongId", "dbo.Phong", "MaPhong");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "PhongId", "dbo.Phong");
            DropIndex("dbo.Reviews", new[] { "PhongId" });
            DropColumn("dbo.Reviews", "PhongId");
        }
    }
}
