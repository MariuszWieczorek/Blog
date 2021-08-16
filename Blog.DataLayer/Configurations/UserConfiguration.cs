using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Blog.DataLayer.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Login)
                  .IsRequired()
                  .HasMaxLength(10);

            builder.Property(x => x.Password)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.HasIndex(x => x.Login)
                .IsUnique();

            // HasOne - wskazujemy właściwość nawigacyjną w Kontaktach
            // WithOne - wskazujemy właściwość nawigacyjną w klasie User
            // HasForeignKey<ContactInfo> wskazujemy klucz obcy w ContactInfo
            builder.HasOne(x => x.ContactInfo)
                .WithOne(x => x.User)
                .HasForeignKey<ContactInfo>(x => x.UserId);

                    

        }
    }
}
