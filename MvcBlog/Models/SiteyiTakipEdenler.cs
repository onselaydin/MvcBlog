namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SiteyiTakipEdenler")]
    public partial class SiteyiTakipEdenler
    {
        [Key]
        public int TakipId { get; set; }

        [Required]
        [StringLength(500)]
        public string MailAdress { get; set; }

        public Guid? YazarId { get; set; }

        public int? KategoriId { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}
