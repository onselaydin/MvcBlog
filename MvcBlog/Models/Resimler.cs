namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resimler")]
    public partial class Resimler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resimler()
        {
            Kullanici1 = new HashSet<Kullanici>();
            Makale = new HashSet<Makale>();
            Makale1 = new HashSet<Makale>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Adi { get; set; }

        [StringLength(500)]
        public string KucukResimYol { get; set; }

        [StringLength(500)]
        public string OrtaResimYol { get; set; }

        [StringLength(500)]
        public string BuyukResimYol { get; set; }

        [StringLength(50)]
        public string VideoYol { get; set; }

        public Guid? EkleyenId { get; set; }

        public DateTime? EklemeTarihi { get; set; }

        public int? Goruntulenme { get; set; }

        public int? Begeni { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanici1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makale> Makale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Makale> Makale1 { get; set; }
    }
}
