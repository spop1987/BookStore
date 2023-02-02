using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Models.Entities.TypeConfig
{
    public class UserTypeConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserName).HasColumnType("varchar(50)");
            builder.Property(u => u.FullName).HasColumnType("varchar(50)");
            builder.Property(u => u.Password).HasColumnType("varchar(50)");
            builder.Property(u => u.RefreshToken).HasColumnType("varchar(50)");
            builder.Property(u => u.RefreshTokenExpiryTime).HasColumnType("varchar(50)");
        }
    }
}