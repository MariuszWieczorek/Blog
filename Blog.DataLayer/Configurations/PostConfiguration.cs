using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer.Configurations
{

    class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {

            builder.ToTable("Posts2"); 
            
            builder.Property(x => x.Title)
               .HasMaxLength(100)
               .HasColumnName("Title2")
               .IsRequired();

            builder.Property(x => x.PostedOn)
               .HasColumnType("datetime");

            
            builder.Property(x => x.ShortDescription)
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(200);

            
            builder.Property(x => x.ImageUrl)
                .IsUnicode(false)
                .HasDefaultValue("/content/image.png");

            // HasOne przekazujemy właściwość nawigacyjną z Post
            // WithMany przekazujemy kolekcję, czyli właściwość nawigacyjną z User: Posts 
            builder.HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // relacja z użytkownikiem, który zatwierdził 
            builder.HasOne(x => x.ApprovedBy)
               .WithMany(x => x.PostsApproved)
               .HasForeignKey(x => x.ApprovedByUserId)
               .OnDelete(DeleteBehavior.ClientSetNull);


            // relacja z kategorią
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // wiele do wielu
            // HasMany - właściwość nawigacyjna
            // nasz post ma wiele tagów
            // a każdy tag ma wiele postów
            // i używamy encji łączącej PostTag
            builder.HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<PostTag>(
                    x => x.HasOne(x => x.Tag).WithMany().HasForeignKey(x => x.TagId),
                    x => x.HasOne(x => x.Post).WithMany().HasForeignKey(x => x.PostId)
                    )
                .Property(x => x.CreatedDate)
                .HasDefaultValueSql("getdate()");



        }
    }
}
