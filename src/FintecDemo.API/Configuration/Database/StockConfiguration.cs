using FintecDemo.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FintecDemo.API.Configuration.Database
{
    public static class StockConfiguration 
    {
        public static ModelBuilder ConfigureStock(this EntityTypeBuilder<Stock> instance, ModelBuilder builder)
        {
            instance
                .ToTable("Stocks")
                .HasKey(stock => stock.Id);

            instance
                .HasIndex(stock => stock.Isin)
                .IsUnique();
            
            instance.Property(stock => stock.Isin)
                .IsRequired();
            
            instance
                .HasOne(stock => stock.CompanyProfile)
                .WithOne(companyProfile => companyProfile.Stock)
                .HasForeignKey<CompanyProfile>(CompanyProfile => CompanyProfile.StockId);
            
            return builder;
        }
    }
}