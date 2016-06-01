namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mesajlar")]
    public partial class Mesajlar
    {
        [Key]
        public int MesajId { get; set; }

        public Guid? GonderenUye { get; set; }

        public Guid? AlanUye { get; set; }

        [StringLength(500)]
        public string Mesaj { get; set; }

        public DateTime? Tarih { get; set; }

        public bool? Goruldu { get; set; }
    }
}
