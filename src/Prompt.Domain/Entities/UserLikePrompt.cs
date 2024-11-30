using Prompt.Domain.Common;
using Prompt.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Domain.Entities
{
    public sealed class UserLikePrompt:EntityBase
    {
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long PromptId { get; set; }
        public Prompt Prompt { get; set; }
    }
}
