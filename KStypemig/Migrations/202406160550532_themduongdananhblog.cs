namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themduongdananhblog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blog", "Duongdananh", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blog", "Duongdananh");
        }
    }
}
