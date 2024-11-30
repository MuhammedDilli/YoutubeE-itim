using Prompt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prompt.Domain.Entities
{
    public sealed class Category:EntityBase
    {
        public string  Name { get; set; }
        public string  Description { get; set; }


        public ICollection<PromptCategory> PromptCategories { get; set; } = [];


    }
}
