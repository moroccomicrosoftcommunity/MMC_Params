using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParamsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParamsService.Infrastructure.Data
{
    public class MMC_Params : DbContext
    {
        public MMC_Params()
        {

        }
        public MMC_Params(DbContextOptions<DbContext> options)
      : base(options)
        {
        }
        
        
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<Mode> Modes { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<Sponsor> Sponsors { get; set; }
        public virtual DbSet<PartnerEvent> PartnerEvent { get; set; }
        public virtual DbSet<SponsorEvent> SponsorEvent { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<ParticipantEvent> ParticipantEvent { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartnerEvent>(e =>
            {
                e.HasKey(p => p.Id);  
            });

            modelBuilder.Entity<SponsorEvent>(e =>
            e.HasNoKey());

            base.OnModelCreating(modelBuilder);
            // Seed data
            // modelBuilder.Entity<City>().HasData(
            //     new City { Id = Guid.Parse("A0EEFAC0-F21F-40F4-99E2-072761E96D11"), Name = "Beni Mellal" },
            //     new City { Id = Guid.Parse("8074CD03-3714-4C7E-B65A-139014BD996E"), Name = "Marrakech" },
            //     new City { Id = Guid.Parse("A1496DBA-9839-45B4-BA1C-18FD3B8C5B6F"), Name = "Mirleft" },
            //     new City { Id = Guid.Parse("D6FF820B-9936-43B5-8B84-26A492DFCEED"), Name = "Ouarzazate" },
            //     new City { Id = Guid.Parse("995B9286-B31F-4CF4-BD9A-2A0C4D2153A6"), Name = "Casablanca" },
            //     new City { Id = Guid.Parse("EBDE21B5-ED59-4AB1-A3C3-375FACA710CD"), Name = "Ben Ahmed" },
            //     new City { Id = Guid.Parse("AF448539-4F42-49DB-877C-43C256537A17"), Name = "El-jadida" },
            //     new City { Id = Guid.Parse("BD682A38-01C4-4AEA-86A6-5B282CA338AB"), Name = "Tetouan" },
            //     new City { Id = Guid.Parse("89A73357-FE96-457B-8E9E-629E4E5CF1CD"), Name = "Agadir" },
            //     new City { Id = Guid.Parse("403DC274-F940-4484-9E6B-772C5C7F1BB2"), Name = "Meknès" },
            //     new City { Id = Guid.Parse("F4BA2ECC-7439-4507-866D-A1523DEDAB4C"), Name = "Rabat" },
            //     new City { Id = Guid.Parse("4D516829-D146-45D0-8BAB-B205DFEF9E82"), Name = "Mohammedia" },
            //     new City { Id = Guid.Parse("46EA3154-6F4F-4225-A2DA-BEB7A19330EA"), Name = "Tanger" },
            //     new City { Id = Guid.Parse("4557787C-D633-427C-AA19-C096D4390C5C"), Name = "Settat" },
            //     new City { Id = Guid.Parse("A959FBF6-3287-4BE1-8157-D1F96ABB22ED"), Name = "Essaouira" },
            //     new City { Id = Guid.Parse("F448BC14-3510-4AC6-9D33-D91F37CF081F"), Name = "Khouribga" }
            // );
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer("data source=portal.snifly.com;initial catalog=MMC_Params;persist security info=True;user id=sa;password=ERP16One;MultipleActiveResultSets=True;Trust Server Certificate=True;");


        
    }
}
