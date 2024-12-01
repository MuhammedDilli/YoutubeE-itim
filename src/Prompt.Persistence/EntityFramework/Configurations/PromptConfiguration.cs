using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prompt.Domain.Entities;

namespace Prompt.Persistence.EntityFramework.Configurations
{
    public sealed class PromptConfiguration : IEntityTypeConfiguration<Prompts>
    {
        public void Configure(EntityTypeBuilder<Prompts> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Title
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Description
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(5000);

            // Content
            builder.Property(x => x.Content)
                .IsRequired();

            // ImageUrl
            builder.Property(x => x.ImageUrl)
                .HasMaxLength(1024)
                .IsRequired(false);

            // IsActive
            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(false);


            // UserFavoritePrompts Relationship
            builder.HasMany(x => x.UserFavoritePrompts)
                .WithOne(ufp => ufp.Prompt)
                .HasForeignKey(ufp => ufp.PromptId);

            // UserLikePrompts Relationship
            builder.HasMany(x => x.UserLikePrompts)
                .WithOne(ulp => ulp.Prompt)
                .HasForeignKey(ulp => ulp.PromptId);

            // Placeholders Relationship
            builder.HasMany(x => x.PlaceHolders)
                .WithOne(p => p.Prompt)
                .HasForeignKey(p => p.PromptId);
            

            // CreatedAt
            builder.Property(p => p.CreatedAt)
                .IsRequired();

            // CreatedByUserId
            builder.Property(p => p.CreatedByUserId)
                .IsRequired(false);
            //.HasMaxLength(150);

            // ModifiedAt
            builder.Property(p => p.ModifiedAt)
                .IsRequired(false);

            // ModifiedByUserId
            builder.Property(p => p.ModifiedByUserId)
                .IsRequired(false);
            //.HasMaxLength(150);

            // Table Name
            builder.ToTable("prompts");
        }
    }
}