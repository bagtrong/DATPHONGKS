namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class them_colum_dichvu : DbMigration
    {
        public override void Up()
        {
            // Thêm cột mới để lưu giá trị tendichvu
            AddColumn("dbo.DatPhongs", "DichVu", c => c.String(maxLength: 20)); // Điều chỉnh maxLength nếu cần

        }

        public override void Down()
        {
            // Xóa cột mới "DichVu"
            DropColumn("dbo.DatPhongs", "DichVu");

        }
    }
}
