using FintecDemo.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FintecDemo.API.Configuration.Database
{
    public static class PersonConfiguration
    {
        public static ModelBuilder ConfigurePerson(this EntityTypeBuilder<Person> instance, ModelBuilder builder)
        {
            instance
                .ToTable("People")
                .HasKey(person => person.Id);

            instance.Property(person => person.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            
            instance.Property(person => person.LastName)
                .IsRequired()
                .HasMaxLength(50);        
            return builder;
        }
        
    }
}