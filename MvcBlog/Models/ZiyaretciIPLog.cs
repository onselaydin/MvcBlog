namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ZiyaretciIPLog")]
    public partial class ZiyaretciIPLog
    {
        [Key]
        public int IpId { get; set; }

        [Required]
        [StringLength(50)]
        public string IpAddress { get; set; }

        public DateTime? Tarih { get; set; }
    }
}
