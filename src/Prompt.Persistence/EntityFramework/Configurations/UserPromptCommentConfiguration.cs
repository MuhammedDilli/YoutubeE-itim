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
    public sealed class UserPromptCommentConfiguration : IEntityTypeConfiguration<UserPromptComment>
    {
        public void Configure(EntityTypeBuilder<UserPromptComment> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Level
            builder.Property(x => x.Level)
                .IsRequired();

            // Content
            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(1000);


            // User Relationship
            builder.HasOne(x => x.User)
                .WithMany(u => u.UserPromptComments)
                .HasForeignKey(x => x.UserId);

            // Parent Comment Relationship
            builder.HasOne(x => x.ParentComment)
                .WithMany(pc => pc.ChildComments)
                .HasForeignKey(x => x.ParentCommentId)
                .IsRequired(false);

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
            builder.ToTable("user_prompt_comments");
        }
    }
}