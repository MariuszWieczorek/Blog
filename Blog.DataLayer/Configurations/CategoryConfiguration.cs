using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Blog.DataLayer.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            // ustawiamy do jakiej tabeli, nic się nie zmieni,
            // bo podaliśmy nazwę zgodną z konwencją
            builder.ToTable("Categories");

            // ustawiamy klucz główny, również się nic nie zmieni
            // bo wg konwencji byłoba to również kolumna Id
            builder.HasKey(x => x.Id);

            // Ustawiamy indeks i unikalność na polu Name
            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(20);

            builder.Property(x => x.Url)
                .HasMaxLength(500);

            builder.Property(x => x.Description)
                .HasMaxLength(20);



        }
    }
}
