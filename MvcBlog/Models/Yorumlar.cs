namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Yorumlar")]
    public partial class Yorumlar
    {
        [Key]
        public int YorumId { get; set; }

        [Required]
        [StringLength(50)]
        public string Baslik { get; set; }

        [Required]
        [StringLength(500)]
        public string Icerik { get; set; }

        public int? MakaleId { get; set; }

        public DateTime? EklemeTarihi { get; set; }

        public Guid? YazarID { get; set; }

        public bool? Aktif { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Makale Makale { get; set; }
    }
}
