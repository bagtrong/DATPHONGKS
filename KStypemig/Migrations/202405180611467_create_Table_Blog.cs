namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_Table_Blog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        id_baiviet = c.Int(nullable: false, identity: true),
                        tieude = c.String(maxLength: 200),
                        noidung = c.String(),
                        ngaydang = c.DateTime(),
                        tacgia = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.id_baiviet);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blog");
        }
    }
}
