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
    public sealed class UserSocialMediaAccountConfiguration : IEntityTypeConfiguration<UserSocialMediaAccount>
    {
        public void Configure(EntityTypeBuilder<UserSocialMediaAccount> builder)
        {
            // Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // SocialMediaType
            builder.Property(x => x.SocialMediaType)
                .HasColumnType("smallint")
                .HasConversion<int>()
                .IsRequired();

            // Url
            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(1024);


            // User Relationship
            builder.HasOne(x => x.User)
                .WithMany(u => u.UserSocialMediaAccounts)
                .HasForeignKey(x => x.UserId);

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
            builder.ToTable("user_social_media_accounts");
        }
    }
}
