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
          

            // właściwość nawigacyjną w klasie User
            // właściwość nawigacyjną w klasie ContactInfo
            // klucz obcy w ContactInfo

            builder
                .HasOne(u => u.ContactInfo)                     
                .WithOne(c => c.User)                           
                .HasForeignKey<ContactInfo>(c => c.UserId);

          


        }
    }
}
