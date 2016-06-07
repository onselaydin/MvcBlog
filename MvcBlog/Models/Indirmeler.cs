using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcBlog.Models
{
    [Table("Indirmeler")]
    public partial class Indirmeler
    {
        
        [Key]
        public int SNo { get; set; }

        [StringLength(50)]
        public string DosyaAdi { get; set; }

        [Column(TypeName = "text")]
        public string DosyaAciklama { get; set; }

        [StringLength(50)]
        public string DosyaBoyutu { get; set; }

        [StringLength(250)]
        public string Link { get; set; }

        public DateTime? EklemeTarihi { get; set; }

    }
}