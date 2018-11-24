using FintecDemo.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FintecDemo.API.Configuration.Database
{
    public static class CompanyProfileConfiguration
    {
        public static ModelBuilder ConfigureCompanyProfile(this EntityTypeBuilder<CompanyProfile> instance, ModelBuilder builder)
        {
            instance
                .ToTable("CompanyProfiles")
                .HasKey(companyProfile => companyProfile.Id);

            instance
                .Property(companyProfile => companyProfile.About)
                .IsRequired();

            instance.Property(companyProfile => companyProfile.Name)
                .IsRequired();

            instance
                .HasOne(companyProfile => companyProfile.Ceo)
                .WithOne(person => person.Company)
                .HasForeignKey<Person>(person => person.CompanyId);
                
                
            return builder;
        }
    }
}