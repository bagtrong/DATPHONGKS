using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    public class AnhChiTietPhong
    {
        [Key]
        [Column("id_anhphongchitiet")]
        public int IdAnhphongchitiet { get; set; }
        [Column("maphong")]
        [StringLength(10)]
      
        public string Maphong { get; set; }
        [Column("urlanh")]
        [StringLength(50)]
        public string Urlanh { get; set; }

        public Phong phong { get; set; }

    }
}