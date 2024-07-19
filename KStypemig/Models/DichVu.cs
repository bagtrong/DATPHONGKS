using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    public class DichVu
    {
        [Key]
        [StringLength(10)]
        
        public string MaDichVu { get; set; }
        [StringLength(20)]
        [Required]
        public string TenDichVu { get; set; } 
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? GiaDichVu { get; set; }

        public ICollection<DatPhong> datPhongs { get; set; }



    }
}