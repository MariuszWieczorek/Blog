using Blog.Domain.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataLayer.Configurations
{
    class UserFullInfoConfiguration : IEntityTypeConfiguration<UserFullInfo>
    {
        public void Configure(EntityTypeBuilder<UserFullInfo> builder)
        {
            builder.HasNoKey().ToView("UserFullInfo");
        }
    }
}
