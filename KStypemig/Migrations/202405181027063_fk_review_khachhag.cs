namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk_review_khachhag : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Reviews", "ID_KhachHang");
            AddForeignKey("dbo.Reviews", "ID_KhachHang", "dbo.KhachHangs", "ID_KhachHang");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ID_KhachHang", "dbo.KhachHangs");
            DropIndex("dbo.Reviews", new[] { "ID_KhachHang" });
        }
    }
}
