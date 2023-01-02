using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.DbRepository.EntityConfiguration.Base;
using Test.Domain.Entities;

namespace Test.DbRepository.EntityConfiguration;

public class CompanyEntityConfiguration: EntityTypeConfiguration<Company>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Company> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.HasIndex(e => e.Code).IsUnique();
    }
}