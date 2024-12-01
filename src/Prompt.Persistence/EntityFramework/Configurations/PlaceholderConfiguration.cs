using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Prompt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Persistence.EntityFramework.Configurations
{
    public sealed class PlaceholderConfiguration : IEntityTypeConfiguration<PlaceHolder>
    {
        public void Configure(EntityTypeBuilder<PlaceHolder> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Name
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            // CreatedAt
            builder.Property(p => p.CreatedAt)
                .IsRequired();

            // CreatedByUserId
            builder.Property(p => p.CreatedByUserId)
                .IsRequired(false)
                .HasMaxLength(100);

            // ModifiedAt
            builder.Property(p => p.ModifiedAt)
                .IsRequired(false);

            // ModifiedByUserId
            builder.Property(p => p.ModifiedByUserId)
                .IsRequired(false)
                .HasMaxLength(100);

            // Table Name
            builder.ToTable("placeholders");
        }
    }
}