using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    public class NhanVien
    {
        [Key]
        [Column("ID_NhanVien")]
        [StringLength(20)]
        //[Unicode(false)]
        public string IdNhanVien { get; set; } 
        [StringLength(20)]
        //[Unicode(false)]
        public string MatKhau { get; set; }
        [StringLength(30)]
        public string HoTen { get; set; }
        [StringLength(20)]
        //[Unicode(false)]
        public string SoDienThoai { get; set; }
        [StringLength(35)]
        //[Unicode(false)]
        public string Email { get; set; }
        public bool? LaAdmin { get; set; }
        [Column("CCCD")]
        [StringLength(12)]
        public string Cccd { get; set; }
        [Column("vaitro")]
        [StringLength(12)]
        public string Vaitro { get; set; }
        public ICollection<DatPhong> datPhongs { get; set; }

    }
}