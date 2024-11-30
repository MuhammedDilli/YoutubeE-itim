using Prompt.Domain.Common;
namespace Prompt.Domain.Entities
{
    public sealed class PlaceHolder :EntityBase
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public long PromptId { get; set; }
        public Prompt Prompt { get; set; }


    }
}
