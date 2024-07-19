namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_tbDichVu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DichVus",
                c => new
                    {
                        MaDichVu = c.String(nullable: false, maxLength: 10),
                        TenDichVu = c.String(nullable: false, maxLength: 20),
                        GiaDichVu = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MaDichVu);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DichVus");
        }
    }
}
