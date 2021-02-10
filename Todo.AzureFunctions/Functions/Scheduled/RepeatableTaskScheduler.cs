using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Todo.AzureFunctions.Services.Interfaces;

namespace Todo.AzureFunctions.Functions.Scheduled
{
    public class RepeatableTaskScheduler
    {
        private readonly ITodoItemService _todoItemService;

        public RepeatableTaskScheduler(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [FunctionName("RepeatableTaskScheduler")]
        public void Run([TimerTrigger("0 * * * * *")]TimerInfo myTimer, ILogger log)
        { var dateDate = DateTime.Now;
            var dayOfTheWeek = dateDate.DayOfWeek;

            var items = _todoItemService.GetAllRepeatable(dayOfTheWeek);

            foreach (var item in items)
            {
                item.Status = false;
                _todoItemService.Save(item);
            }
        }
    }
}
