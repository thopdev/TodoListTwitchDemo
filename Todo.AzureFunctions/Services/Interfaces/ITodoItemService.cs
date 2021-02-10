using System;
using System.Collections.Generic;
using Todo.AzureFunctions.Entities;

namespace Todo.AzureFunctions.Services.Interfaces
{
    public interface ITodoItemService : ICloudTableServiceBase<TodoItemEntity>
    {
        List<TodoItemEntity> GetAllRepeatable(DayOfWeek dayOfWeek);
    }
}