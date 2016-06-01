namespace MvcBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BlogModel : DbContext
    {
        public BlogModel()
            : base("name=BlogModel")
        {
        }

        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<Etiket> Etiket { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Makale> Makale { get; set; }
        public virtual DbSet<MakaleTipleri> MakaleTipleri { get; set; }
        public virtual DbSet<Mesajlar> Mesajlar { get; set; }
        public virtual DbSet<Resimler> Resimler { get; set; }
        public virtual DbSet<SiteyiTakipEdenler> SiteyiTakipEdenler { get; set; }
        public virtual DbSet<Yorumlar> Yorumlar { get; set; }
        public virtual DbSet<ZiyaretciIPLog> ZiyaretciIPLog { get; set; }
        public virtual DbSet<Saying> Saying { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Paths)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Roles)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Paths>()
                .HasOptional(e => e.aspnet_PersonalizationAllUsers)
                .WithRequired(e => e.aspnet_Paths);

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.aspnet_Users)
                .WithMany(e => e.aspnet_Roles)
                .Map(m => m.ToTable("aspnet_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Profile)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventSequence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventOccurrence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<Etiket>()
                .HasMany(e => e.Makale)
                .WithMany(e => e.Etiket)
                .Map(m => m.ToTable("MakaleEtiketleri").MapLeftKey("EtiketId").MapRightKey("MakaleEtiketId"));

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Resimler)
                .WithOptional(e => e.Kullanici)
                .HasForeignKey(e => e.EkleyenId);

            modelBuilder.Entity<Makale>()
                .Property(e => e.Icerik)
                .IsUnicode(false);

            modelBuilder.Entity<Makale>()
                .HasMany(e => e.Resimler1)
                .WithMany(e => e.Makale1)
                .Map(m => m.ToTable("MakaleResimleri").MapLeftKey("MakaleId").MapRightKey("ResimId"));

            modelBuilder.Entity<MakaleTipleri>()
                .HasMany(e => e.Makale)
                .WithRequired(e => e.MakaleTipleri)
                .HasForeignKey(e => e.MakaleTipiId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MakaleTipleri>()
                .HasMany(e => e.Makale1)
                .WithRequired(e => e.MakaleTipleri1)
                .HasForeignKey(e => e.MakaleTipiId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resimler>()
                .HasMany(e => e.Kullanici1)
                .WithOptional(e => e.Resimler1)
                .HasForeignKey(e => e.ResimId);

            modelBuilder.Entity<Resimler>()
                .HasMany(e => e.Makale)
                .WithOptional(e => e.Resimler)
                .HasForeignKey(e => e.KapakResimId);
            
        }
    }
}
