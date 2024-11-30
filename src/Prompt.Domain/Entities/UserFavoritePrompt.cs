using Prompt.Domain.Common;
using Prompt.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Domain.Entities
{
    public sealed class UserFavoritePrompt:EntityBase
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid PromptId { get; set; }
        public Prompts Prompt { get; set; }
    }
}
