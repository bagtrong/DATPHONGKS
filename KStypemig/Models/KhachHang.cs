using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    public class KhachHang
    {
        [Key]
        [Column("ID_KhachHang")]
        [StringLength(20)]
        
        public string IdKhachHang { get; set; }
        [StringLength(20)]
        public string TenTaiKhoan { get; set; }
        [StringLength(20)]
        
        public string MatKhau { get; set; }
        [StringLength(30)]
        public string HoTen { get; set; }
        [StringLength(20)]
        
        public string SoDienThoai { get; set; }
        [StringLength(35)]
        
        public string Email { get; set; }
        [Column("CCCD")]
        [StringLength(12)]
        public string Cccd { get; set; }
        public ICollection<DatPhong> datPhongs { get; set; }

        public ICollection<Review> reviews { get; set; }


    }
}