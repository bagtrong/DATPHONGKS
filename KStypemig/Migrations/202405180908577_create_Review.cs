namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_Review : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ID_KhachHang = c.String(maxLength: 20),
                        Username = c.String(maxLength: 20),
                        Email = c.String(maxLength: 35),
                        Rate = c.Int(),
                        Content = c.String(),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}
