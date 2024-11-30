using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Prompt.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Persistence.EntityFramework.Configurations
{
    public sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>

    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)

        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });


            // Maps to the AspNetUserRoles table
            builder.ToTable("application_user_roles");
        }

    }
}