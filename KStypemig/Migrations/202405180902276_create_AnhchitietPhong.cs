namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_AnhchitietPhong : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnhChiTietPhongs",
                c => new
                    {
                        id_anhphongchitiet = c.Int(nullable: false, identity: true),
                        maphong = c.String(maxLength: 10),
                        urlanh = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.id_anhphongchitiet);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnhChiTietPhongs");
        }
    }
}
