using Microsoft.AspNetCore.Identity;
using Prompt.Domain.Common;
using Prompt.Domain.Entities;
using Prompt.Domain.ValueObject;
namespace Prompt.Domain.Identity
{
    public sealed class ApplicationUser : IdentityUser<Guid>, ICreatedByEntity, IModifiedByEntity
    {
        public FullName FullName { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } 
        public string? ModifiedByUserId { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }





        public ICollection<UserSocialMediaAccount> UserSocialMediaAccounts { get; set; } = [];

        public ICollection<UserPromptComment> UserPromptComments { get; set; } = [];
        public ICollection<UserFavoritePrompt> UserFavoritePrompts { get; set; } = [];
        public ICollection<UserLikePrompt> UserLikePrompts { get; set; } = [];
    }
}
