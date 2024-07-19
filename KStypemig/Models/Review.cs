using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Column("ID_KhachHang")]
        [StringLength(20)]
        
        public string IdKhachHang { get; set; }
        [StringLength(20)]
        
        public string Username { get; set; }

        [ForeignKey("phongs")]
        [StringLength(10)]
        public string PhongId { get; set; }
        [StringLength(35)]
        

        public string Email { get; set; }
        public int? Rate { get; set; }
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }

        public KhachHang KhachHang { get; set; }

        public Phong phongs { get; set; }


    }
}