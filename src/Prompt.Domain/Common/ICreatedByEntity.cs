﻿namespace Prompt.Domain.Common
{
    public interface ICreatedByEntity
    {
        string? CreatedByUserId { get; set; }
        DateTimeOffset CreatedAt { get; set; } // 
    }
}
