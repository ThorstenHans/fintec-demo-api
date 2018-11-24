using FintecDemo.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FintecDemo.API.Configuration.Database
{
    public static class ExchangeConfiguration
    {
        public static ModelBuilder ConfigureExchange(this EntityTypeBuilder<Exchange> instance, ModelBuilder builder)
        {
            instance
                .ToTable("Exchanges")
                .HasKey(exchange => exchange.Id);
            instance
                .HasIndex(exchange => exchange.Shortcut)
                .IsUnique();

            instance.Property(exchange => exchange.Shortcut)
                .IsRequired()
                .HasMaxLength(10);

            instance.Property(exchange => exchange.Name)
                .IsRequired();

            instance.Property(exchange => exchange.Country)
                .IsRequired();
            
            instance
                .Property(p => p.LastModified)
                .HasField("_lastModified")
                .ValueGeneratedOnAddOrUpdate();
            return builder;
        }


    }
}
