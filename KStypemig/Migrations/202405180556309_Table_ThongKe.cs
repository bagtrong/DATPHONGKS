namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_ThongKe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongKes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        thoigian = c.DateTime(storeType: "date"),
                        SoTruyCap = c.Long(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ThongKes");
        }
    }
}
