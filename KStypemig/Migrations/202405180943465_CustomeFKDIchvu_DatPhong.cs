namespace KStypemig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomeFKDIchvu_DatPhong : DbMigration
    {
        public override void Up()
        {
           
            DropForeignKey("dbo.DatPhongs", "dichvus_MaDichVu", "dbo.DichVus");
            DropIndex("dbo.DatPhongs", new[] { "dichvus_MaDichVu" });

            // Xóa cột hiện tại nếu cần thiết
            DropColumn("dbo.DatPhongs", "dichvus_MaDichVu");

            // Thêm cột mới để lưu giá trị tendichvu
            //       AddColumn("dbo.DatPhongs", "DichVu", c => c.String(maxLength: 20)); // Điều chỉnh maxLength nếu cần

            // Tạo chỉ mục và khóa ngoại mới
            CreateIndex("dbo.DichVus", "TenDichVu", unique: true, name: "IX_TenDichVu");

          //  CreateIndex("dbo.DatPhongs", "DichVu");
            AddForeignKey("dbo.DatPhongs", "DichVu", "dbo.DichVus", "TenDichVu");

        }

        public override void Down()
        {
            // Xóa khóa ngoại và chỉ mục mới
            DropForeignKey("dbo.DatPhongs", "DichVu", "dbo.DichVus");
           // DropIndex("dbo.DatPhongs", new[] { "DichVu" });
            DropIndex("dbo.DichVus", "IX_TenDichVu");
            // Xóa cột mới
            //    DropColumn("dbo.DatPhongs", "dichvus_TenDichVu");

            // Thêm lại cột cũ
            AddColumn("dbo.DatPhongs", "dichvus_MaDichVu", c => c.String(maxLength: 10));

            // Tạo lại chỉ mục và khóa ngoại cũ
            CreateIndex("dbo.DatPhongs", "dichvus_MaDichVu");
            AddForeignKey("dbo.DatPhongs", "dichvus_MaDichVu", "dbo.DichVus", "MaDichVu");

        }
    }
}
