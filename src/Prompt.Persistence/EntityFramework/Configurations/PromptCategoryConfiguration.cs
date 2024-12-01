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
    public sealed class PromptCategoryConfiguration : IEntityTypeConfiguration<PromptCategory>
    {
        public void Configure(EntityTypeBuilder<PromptCategory> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Prompt Relationship
            builder.HasOne(x => x.Prompt)
                .WithMany(p => p.PromptCategories)
                .HasForeignKey(x => x.PromptId)
                .IsRequired();

            // Category Relationship
            builder.HasOne(x => x.Category)
                .WithMany(c => c.PromptCategories)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            // Unique Constraint for Prompt and Category Combination
            builder.HasIndex(x => new { x.PromptId, x.CategoryId })
                .IsUnique();

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
            builder.ToTable("prompt_categories");
        }
    }
}