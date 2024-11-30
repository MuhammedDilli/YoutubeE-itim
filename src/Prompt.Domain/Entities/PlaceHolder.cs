using Prompt.Domain.Common;
namespace Prompt.Domain.Entities
{
    public sealed class PlaceHolder :EntityBase
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Guid PromptId { get; set; }
        public Prompts Prompt { get; set; }


    }
}
