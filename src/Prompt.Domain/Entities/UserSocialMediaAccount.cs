using Prompt.Domain.Common;
using Prompt.Domain.Enums;
using Prompt.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Domain.Entities
{
    public sealed class UserSocialMediaAccount :EntityBase
    {
        public SocialMediaType SocialMediaType { get; set; }
        public string?  Url { get; set; }
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
