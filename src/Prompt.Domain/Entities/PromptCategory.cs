using Prompt.Domain.Common;
namespace Prompt.Domain.Entities
{
    public sealed class PromptCategory :EntityBase
    {
        public Guid PromptId { get; set; }
        public Prompts Prompt { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
