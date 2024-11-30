using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Prompt.Domain.Entities;
using Prompt.Domain.Identity;
using Prompt.Persistence.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Persistence.EntityFramework.Contexts
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<PlaceHolder> Placeholders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Prompts> Prompts { get; set; }
        public DbSet<PromptCategory> PromptCategories { get; set; }
        public DbSet<UserSocialMediaAccount> UserSocialMediaAccounts { get; set; }
        public DbSet<UserPromptComment> UserPromptComments { get; set; }
        public DbSet<UserFavoritePrompt> UserFavoritePrompts { get; set; }
        public DbSet<UserLikePrompt> UserLikePrompts { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.ToSnakeCaseNames();
        }

    }
}


