using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Models.Entities.TypeConfig
{
    public class PersonTypeConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasKey(p => p.PersonId);
            builder.Property(p => p.FirstName).HasColumnType("varchar(50)");
            builder.Property(p => p.LastName).HasColumnType("varchar(50)");
            builder.Property(p => p.Address).HasColumnType("varchar(150)");
            builder.Property(p => p.PhoneNumber).HasColumnType("varchar(15)");
            builder.Property(p => p.Email).HasColumnType("varchar(50)");
        }
    }
}