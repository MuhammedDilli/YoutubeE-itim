using Prompt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Domain.Entities
{
    public sealed class PromptCategory :EntityBase
    {
        public long PromptId { get; set; }
        public Prompt Prompt { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
