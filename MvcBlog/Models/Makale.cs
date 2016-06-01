namespace MvcBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Makale")]
    public partial class Makale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Makale()
        {
            Yorumlar = new HashSet<Yorumlar>();
            Etiket = new HashSet<Etiket>();
            Resimler1 = new HashSet<Resimler>();
        }

        public int MakaleId { get; set; }

        [Required]
        [StringLength(150)]
        public string Baslik { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Icerik { get; set; }

        public DateTime? YayimTarihi { get; set; }

        public int? KategoriId { get; set; }

        public int MakaleTipiId { get; set; }

        public Guid? YazarId { get; set; }

        public int? KapakResimId { get; set; }

        public int? Goruntulenme { get; set; }

        public int? Begeni { get; set; }

        public bool? Aktif { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual MakaleTipleri MakaleTipleri { get; set; }

        public virtual MakaleTipleri MakaleTipleri1 { get; set; }

        public virtual Resimler Resimler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Yorumlar> Yorumlar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etiket> Etiket { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resimler> Resimler1 { get; set; }
    }
}
