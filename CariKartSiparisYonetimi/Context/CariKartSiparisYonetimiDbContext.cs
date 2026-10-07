using CariKartSiparisYonetimi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CariKartSiparisYonetimi.Context
{
    public class CariKartSiparisYonetimiDbContext:DbContext
    {
        public CariKartSiparisYonetimiDbContext(DbContextOptions<CariKartSiparisYonetimiDbContext> options) : base(options)
        {
        }

        public DbSet<CariKart> CariKartlar => Set<CariKart>();
        public DbSet<Siparis> Siparisler => Set<Siparis>();
        public DbSet<SiparisDetayi> SiparisDetaylari => Set<SiparisDetayi>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Carikod tekrar ve sipariş no benzersiz
            modelBuilder.Entity<CariKart>().HasIndex(x => x.CariKod).IsUnique();

            modelBuilder.Entity<Siparis>().HasIndex(s => s.SiparisNo) .IsUnique();

            //Carikartın siparişi varsa silinemez
            modelBuilder.Entity<Siparis>().
                HasOne(s=>s.CariKart)
                .WithMany(c=>c.Siparisler)
                .HasForeignKey(s=>s.CariKartId)
                .OnDelete(DeleteBehavior.Restrict);


            // Sipariş silinirse detaylar da birlikte silinir.
            modelBuilder.Entity<SiparisDetayi>()
                .HasOne(d => d.Siparis)
                .WithMany(s => s.Detaylar)
                .HasForeignKey(d => d.SiparisId)
                .OnDelete(DeleteBehavior.Cascade);

       

            // SQL Server'da decimal kolonlar: toplam 18 basamak, 2'si ondalık.
            modelBuilder.Entity<SiparisDetayi>().Property(d => d.Miktar).HasPrecision(18, 2);
            modelBuilder.Entity<SiparisDetayi>().Property(d => d.BirimFiyat).HasPrecision(18, 2);
            modelBuilder.Entity<SiparisDetayi>().Property(d => d.Tutar).HasPrecision(18, 2);


        }
    }
}
