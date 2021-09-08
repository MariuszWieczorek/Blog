using Blog.Domain.Entities.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer.Configurations.Queries
{
    public class CustomConfiguration : IEntityTypeConfiguration<Custom>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Custom> builder)
        {
            builder.HasNoKey().ToView("Custom");
        }
    }
}
