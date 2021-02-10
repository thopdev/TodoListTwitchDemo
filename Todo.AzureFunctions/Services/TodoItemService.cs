using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Todo.AzureFunctions.Entities;
using Todo.AzureFunctions.Factories.Factories;
using Todo.AzureFunctions.Services.Interfaces;

namespace Todo.AzureFunctions.Services
{
    public class TodoItemService : CloudTableServiceBase<TodoItemEntity>, ITodoItemService
    {
        public TodoItemService(ICloudTableFactory cloudTableFactory) : base(cloudTableFactory)
        {
        }

        public List<TodoItemEntity> GetAllRepeatable(DayOfWeek dayOfWeek)
        {
            var query = CloudTable.CreateQuery<TodoItemEntity>().Where(x => x.Status);


            var queryable = (dayOfWeek) switch
            {
                DayOfWeek.Sunday => query.Where(x => x.Sunday),
                DayOfWeek.Monday => query.Where(x => x.Monday),
                DayOfWeek.Tuesday => query.Where(x => x.Tuesday),
                DayOfWeek.Wednesday => query.Where(x => x.Wednesday),
                DayOfWeek.Thursday => query.Where(x => x.Thursday),
                DayOfWeek.Friday => query.Where(x => x.Friday),
                DayOfWeek.Saturday => query.Where(x => x.Saturday),
                _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
            };
             return queryable.ToList();
        }
    }

}
