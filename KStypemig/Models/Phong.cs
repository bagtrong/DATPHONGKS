using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    [Table("Phong")]


    public class Phong
    {
        [Key]
        [StringLength(10)]
        
        public string MaPhong { get; set; } 
        [StringLength(10)]
        
        public string TenPhong { get; set; }
        [StringLength(10)]
        
        public string MaLoai { get; set; }
        public int? DienTich { get; set; }
        public int? GiaThue { get; set; }
        [Column("sogiuong")]
        public int? Sogiuong { get; set; }
        [Column("ghichu")]
        [StringLength(100)]
        public string Ghichu { get; set; }
        [Column("tinhtrangphong")]
        [StringLength(10)]
        public string Tinhtrangphong { get; set; }
        public int? Songuoitoida { get; set; }

        public ICollection<DatPhong> datPhongs { get; set; }
        public ICollection<AnhChiTietPhong> anhChiTietPhongs { get; set; }

        public LoaiPhong LoaiPhong { get; set; }

        public ICollection<Review> reviews { get; set; }
    }
}