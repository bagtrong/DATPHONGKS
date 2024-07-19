using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        [Column("id_baiviet")]
        public int IdBaiviet { get; set; }
        [Column("tieude")]
        [StringLength(200)]
        public string Tieude { get; set; }
        [Column("noidung")]
        public string Noidung { get; set; }
        [Column("ngaydang", TypeName = "datetime")]
        public DateTime? Ngaydang { get; set; }
        [Column("tacgia")]
        [StringLength(20)]
        public string Tacgia { get; set; }
        [Column("Duongdananh")]
        public string Duongdananh { get; set; }

    }
}