﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Prompt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Persistence.EntityFramework.Configurations
{
    public sealed class UserLikePromptConfiguration : IEntityTypeConfiguration<UserLikePrompt>
    {
        public void Configure(EntityTypeBuilder<UserLikePrompt> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // User Relationship
            builder.HasOne(x => x.User)
                .WithMany(u => u.UserLikePrompts)
                .HasForeignKey(x => x.UserId);

            // Prompt Relationship
            builder.HasOne(x => x.Prompt)
                .WithMany(p => p.UserLikePrompts)
                .HasForeignKey(x => x.PromptId);

            // Unique Constraint for User and Prompt Combination
            builder.HasIndex(x => new { x.UserId, x.PromptId })
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
            builder.ToTable("user_like_prompts");
        }
    }
}