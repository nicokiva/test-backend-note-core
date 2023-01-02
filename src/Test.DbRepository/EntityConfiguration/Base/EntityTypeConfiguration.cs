using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test.Domain.Seed;

namespace Test.DbRepository.EntityConfiguration.Base
{
    public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity 
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            
            var converter = new ValueConverter<byte[], long>(
                    v => BitConverter.ToInt64(v, 0),
                    v => BitConverter.GetBytes(v));

            if (UseConcurrencyToken)
                builder.UseXminAsConcurrencyToken();

            if (UseAuditory)
            {
                builder.Property(e => e.UpdatedAt)
                    .ValueGeneratedOnAddOrUpdate()
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasDefaultValueSql("NOW()");

                builder.Property(e => e.CreatedAt)
                    .ValueGeneratedOnAdd()
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasDefaultValueSql("NOW()");

                builder.Property(e => e.CreatedBy)
                    .IsRequired(false)
                    .HasMaxLength(256);

                builder.Property(e => e.UpdatedBy)
                    .IsRequired(false)
                    .HasMaxLength(256);
            }


            ConfigureEntity(builder);
        }

        protected virtual bool UseConcurrencyToken => true;
        protected virtual bool UseAuditory => true;


        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
