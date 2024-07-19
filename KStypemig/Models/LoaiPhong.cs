using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    public class LoaiPhong
    {
        [Key]
        [StringLength(10)]
        
        public string MaLoai { get; set; }
        [StringLength(30)]
        public string TenLoai { get; set; }
        [StringLength(250)]
        public string GhiChu { get; set; }
        [StringLength(50)]
        
        public string DuongDanAnh { get; set; }
        public int? SoluongPhong { get; set; }

        public ICollection<Phong> phongs { get; set; }

    }
}