using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KStypemig.Models
{
    public class ThongKe
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("thoigian", TypeName = "date")]
        public DateTime? Thoigian { get; set; }
        public long? SoTruyCap { get; set; }
    }
}