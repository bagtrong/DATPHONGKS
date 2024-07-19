namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dc_fkdichvu_datph : DbMigration
    {
        public override void Up()
        {
            // Xóa khóa ngoại và chỉ mục mới
            DropForeignKey("dbo.DatPhongs", "DichVu", "dbo.DichVus");
            DropIndex("dbo.DatPhongs", new[] { "DichVu" });

            // Xóa cột mới "DichVu"
            DropColumn("dbo.DatPhongs", "DichVu");

            // Thêm lại cột cũ "dichvus_MaDichVu"
            AddColumn("dbo.DatPhongs", "dichvus_MaDichVu", c => c.String(maxLength: 10));

            // Tạo lại chỉ mục và khóa ngoại cũ
            CreateIndex("dbo.DatPhongs", "dichvus_MaDichVu");
            AddForeignKey("dbo.DatPhongs", "dichvus_MaDichVu", "dbo.DichVus", "MaDichVu");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatPhongs", "dichvus_MaDichVu", "dbo.DichVus");
            DropIndex("dbo.DatPhongs", new[] { "dichvus_MaDichVu" });

            // Xóa cột hiện tại nếu cần thiết
            DropColumn("dbo.DatPhongs", "dichvus_MaDichVu");

            // Thêm cột mới để lưu giá trị tendichvu
            AddColumn("dbo.DatPhongs", "DichVu", c => c.String(maxLength: 20)); // Điều chỉnh maxLength nếu cần

            // Tạo chỉ mục và khóa ngoại mới
            CreateIndex("dbo.DatPhongs", "DichVu", unique: false, name: "IX_DichVu");
            AddForeignKey("dbo.DatPhongs", "DichVu", "dbo.DichVus",  "TenDichVu");
        }
    }
}
