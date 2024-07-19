using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    
    public class DatPhong
    {
        [Key]
        public int MaDatPhong { get; set; }
        [Column("ID_KhachHang")]
        [StringLength(20)]
        
        public string IdKhachHang { get; set; }
        [StringLength(30)]
        //[Required(ErrorMessage ="Ten bat buoc")]
        public string TenKhachHang { get; set; }

       // [Required(ErrorMessage = "Căn cước cd bat buoc")]

        [Column("cancuoccd")]
        [StringLength(12)]
        
        public string Cancuoccd { get; set; }
        
        
        [Column("ID_NhanVien")]
        [StringLength(20)]
        
        public string IdNhanVien { get; set; }
        [StringLength(10)]
        
        public string MaPhong { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayDat { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayDenDuDinh { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayTraDuDinh { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayDenthucte { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayTrathucte { get; set; }
        [Column("thoigianthanhtoan", TypeName = "date")]
        public DateTime? Thoigianthanhtoan { get; set; }
        [Column("tinhtrangthanhtoan")]
        [StringLength(20)]
        public string Tinhtrangthanhtoan { get; set; }
        [Column("phuongthucthanhtoan")]
        [StringLength(20)]
        
        public string Phuongthucthanhtoan { get; set; }
        [StringLength(20)]
        public string DichVu { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ThanhTien { get; set; }

        //[ForeignKey("dichvus")]
        //public string dichvus_MaDichVu { get; set; } // Sử dụng khóa ngoại đã có trong cơ sở dữ liệu


        public Phong phongs { get; set; }
        public DichVu dichvus { get; set; }
        public NhanVien nhanviens { get; set; }
        public KhachHang khachangs { get; set; }



    }
}