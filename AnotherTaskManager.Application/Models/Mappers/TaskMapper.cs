using System;
using AnotherTaskManager.App.Models.DataModels;
using AnotherTaskManager.App.Models.Mappers.Interfaces;
using AnotherTaskManager.App.ViewModels.Task;

namespace AnotherTaskManager.App.Models.Mappers
{
    public static class TaskMapper
    {
        public static Task ConvertToModel(TaskViewModel vm)
        { 
            return new Task
            {
                Id = vm.Id,
                Name = vm.Name,
                Status = vm.StatusId,
                StatusChanges = vm.StatusChanges
            };
        }

        public static TaskViewModel ConvertToViewModel (Task task){
            return new TaskViewModel
            {
                Id = task.Id,
                Name = task.Name,
                StatusDescription = task.StatusNavigation.Description,
                StatusId = task.Status,
                StatusChanges = task.StatusChanges,
                DateCreated = task.DateCreated,
                DateModified = task.DateModified
            };
        }
    }
}
