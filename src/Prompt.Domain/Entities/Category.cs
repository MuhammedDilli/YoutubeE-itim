using Prompt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSID.Creator.NET;

namespace Prompt.Domain.Entities
{
    public sealed class Category:EntityBase
    {
        public string  Name { get; set; }
        public string  Description { get; set; }


        public ICollection<PromptCategory> PromptCategories { get; set; } = [];

        public static Category Create(string name, string description)
        {
            return new Category {
                
             Id=TsidCreator.GetTsid().ToLong(),
             Name=name,
             Description=description,          
            };
        }
    }
}
